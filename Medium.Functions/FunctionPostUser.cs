using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Medium.Application;
using AutoMapper;
using Medium.Functions.Models;
using System;
using System.Threading.Tasks;

namespace Medium.Functions
{
    public class FunctionPostUser
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FunctionPostUser(IUserService userService)
        {
            _userService = userService;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserPost, User>());
            _mapper = config.CreateMapper();
        }

        [FunctionName("FunctionPostUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/users")] HttpRequest request,
            ILogger log)
        {
            log.LogInformation($"FunctionPostUser acessado, executando método {request.Method}.");

            var validator = await request.GetJsonBody<UserPost, UserPostValidator>();

            if (!validator.IsValid)
            {     
                log.LogWarning("Body do HttpRequest inválido!");
                return validator.ToBadRequest();
            }

            var body = new StreamReader(request.Body).ReadToEndAsync().Result;
            var user = JsonSerializer.Deserialize<UserPost>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var result = _userService.Register(_mapper.Map<User>(user));

            return new CreatedResult(
                new Uri($"v1/users/{result.Id}", UriKind.Relative),
                new { result.Id });
        }
    }
}

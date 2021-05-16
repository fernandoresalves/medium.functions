using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Medium.Functions
{
    public static class HttpRequestExtensions
    {
        public static async Task<ValidatableRequest<T>> GetJsonBody<T, TValidator>(this HttpRequest request)
            where TValidator : AbstractValidator<T>, new()
        {
            var requestBody = await request.GetJsonBody<T>();
            var validator = new TValidator();
            var validationResult = validator.Validate(requestBody);

            if (!validationResult.IsValid)
            {
                return new ValidatableRequest<T>
                {
                    Value = requestBody,
                    IsValid = false,
                    Errors = validationResult.Errors
                };
            }

            return new ValidatableRequest<T>
            {
                Value = requestBody,
                IsValid = true
            };
        }


        public static async Task<T> GetJsonBody<T>(this HttpRequest request)
        {
            var requestBody = await request.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }
}

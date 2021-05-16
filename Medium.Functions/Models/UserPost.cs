using FluentValidation;

namespace Medium.Functions.Models
{
    public class UserPost
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserPostValidator : AbstractValidator<UserPost>
    {
        public UserPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();   
        }
    }
}

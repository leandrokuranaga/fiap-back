using Fiap.Application.Auth.Models.Request;
using FluentValidation;

namespace Fiap.Application.Validators.AuthValidators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
              .NotEmpty()
              .WithMessage("Username is required.")
              .EmailAddress()
              .WithMessage("Username must be a valid email address.")
              .MaximumLength(100)
              .WithMessage("Username must be less than or equal to 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

           
        }
    }
}

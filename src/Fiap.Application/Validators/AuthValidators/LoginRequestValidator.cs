using Fiap.Application.Auth.Models.Request;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Validators.AuthValidators
{
    [ExcludeFromCodeCoverage]
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
              .NotEmpty()
              .WithMessage("Username is required.")
              .EmailAddress()
              .WithMessage("Username must be a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");           
        }
    }
}

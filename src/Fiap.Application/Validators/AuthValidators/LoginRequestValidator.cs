using Fiap.Application.Auth.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
              .WithMessage("Email must be a valid email address.")
              .MaximumLength(100)
              .WithMessage("Email must be less than or equal to 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

           
        }
    }
}

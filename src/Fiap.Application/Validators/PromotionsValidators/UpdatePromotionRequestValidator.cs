using Fiap.Application.Promotions.Models.Request;
using FluentValidation;

namespace Fiap.Application.Validators.PromotionsValidators
{
    public class UpdatePromotionRequestValidator : AbstractValidator<UpdatePromotionRequest>
    {
        public UpdatePromotionRequestValidator() 
        { 
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.")
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");


            When(x => x.Discount != null, () =>
            {
                RuleFor(x => x.Discount.Value)
                    .GreaterThan(0)
                    .WithMessage("Discount must be greater than 0.")
                    .LessThanOrEqualTo(100)
                    .WithMessage("Discount must be less than or equal to 100.");
            });

            When(x => x.ExpirationDate != null, () =>
            {
                RuleFor(x => x.ExpirationDate.Value)
                    .GreaterThan(DateTime.UtcNow)
                    .WithMessage("Expiration date must be in the future.");
            });

            When(x => x.GameId != null, () =>
            {
                RuleFor(x => x.GameId)
                    .Must(list => list.Any())
                    .WithMessage("At least one GameId is required.")
                    .Must(list => list.All(id => id.HasValue))
                    .WithMessage("All GameIds must be valid integers.");
            });
        }
    }
}

using Fiap.Application.Validators;
using Fiap.Domain.SeedWork;
using Fiap.Domain.SeedWork.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Common
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseService(INotification notification)
    {
        protected readonly INotification _notification = notification;
        public virtual ValidationResult ValidationResult { get; protected set; }


        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (ValidatorException e)
            {
                _notification.AddNotification("Validator Error", e.Message, NotificationModel.ENotificationType.BadRequestError);
            }
            return default;
        }

        protected virtual void Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);

            if (!ValidationResult.IsValid)
            {
                foreach (var error in ValidationResult.Errors)
                {
                    _notification.AddNotification(error.PropertyName, error.ErrorMessage, NotificationModel.ENotificationType.BadRequestError);
                }
                throw new ValidatorException();
            }
        }
    }
}

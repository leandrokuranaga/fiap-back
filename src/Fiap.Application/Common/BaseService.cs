using Fiap.Application.Validators;
using Fiap.Domain.SeedWork;
using Fiap.Domain.SeedWork.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Fiap.Application.Common
{
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
            catch (NotFoundException e)
            {
                _notification.AddNotification("Not Found", e.Message, NotificationModel.ENotificationType.NotFound);
            }
            catch (ArgumentException e)
            {
                _notification.AddNotification("Invalid Property", e.Message, NotificationModel.ENotificationType.BadRequestError);
            }
            catch (Exception e)
            {
                _notification.AddNotification("Internal Error", e.Message, NotificationModel.ENotificationType.InternalServerError);
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

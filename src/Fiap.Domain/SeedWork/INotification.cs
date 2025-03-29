using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Domain.SeedWork
{
    public interface INotification
    {
        NotificationModel NotificationModel { get; }
        bool HasNotification { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
    }
}

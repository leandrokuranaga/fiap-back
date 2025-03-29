using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Domain.SeedWork
{
    public class Notification : INotification
    {
        public NotificationModel _notification;

        public bool HasNotification => _notification != null;

        public NotificationModel NotificationModel => _notification;

        public void AddNotification(string key, string message, ENotificationType notificationType)
        {
            _notification = new NotificationModel(key, message, notificationType);
        }
    }
}

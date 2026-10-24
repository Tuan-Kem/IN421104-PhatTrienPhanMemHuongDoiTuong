namespace Ch02.Refactor.Notifications;

public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
    
}   
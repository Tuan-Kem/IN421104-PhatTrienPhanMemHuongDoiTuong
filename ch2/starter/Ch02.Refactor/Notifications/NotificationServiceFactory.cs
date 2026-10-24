namespace Ch02.Refactor.Notifications;

public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel)
    {
        return channel switch
        {
            NotificationChannel.Email => new EmailNotificationService(),
            NotificationChannel.SMS => new SmsNotificationService(),
            NotificationChannel.Slack => new SlackNotificationService(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel), $"Unsupported notification channel: {channel}")
        };
    }
}
namespace Ch02.Refactor.Notifications;
public interface INotificationService
{
    void Send(string to, string message);
}
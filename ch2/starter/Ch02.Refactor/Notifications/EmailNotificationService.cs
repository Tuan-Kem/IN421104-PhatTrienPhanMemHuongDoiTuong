namespace Ch02.Refactor.Notifications;
/// cài đặt cho mọi loại email — thay cho chuỗi if/else Gmail/Yahoo của starter.
public class EmailNotificationService : INotificationService
{
    public void Send(string to, string message) => 
        Console.WriteLine($"  [EMAIL] to {to}: {message}");
}
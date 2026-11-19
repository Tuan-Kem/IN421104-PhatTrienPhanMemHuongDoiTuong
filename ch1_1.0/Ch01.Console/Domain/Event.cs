namespace EduHub.Ch01.Domain;

/// <summary>Sự kiện do câu lạc bộ tổ chức.</summary>
public class Event
{
    // TODO: Guid Id, Guid ClubId, string Title, DateTime Date, int Capacity, bool IsOnline (private set)
    public Guid Id { get; private set; }
    public Guid ClubId { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }
    public bool IsOnline { get; private set; }
    // TODO: constructor PRIVATE + public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
    private Event(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = Guid.NewGuid();
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }
    //       → validate: title không trống, capacity > 0

    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline = false)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId không hợp lệ.", nameof(clubId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Tiêu đề sự kiện không được để trống.", nameof(title));

        if (capacity <= 0)
            throw new ArgumentException("Sức chứa phải lớn hơn 0.", nameof(capacity));

        return new Event(clubId, title, date, capacity, isOnline);
    }
    // TODO: public bool HasCapacity(int registeredCount) — true nếu registeredCount < Capacity
    public bool HasCapacity(int registeredCount) => registeredCount < Capacity;
}

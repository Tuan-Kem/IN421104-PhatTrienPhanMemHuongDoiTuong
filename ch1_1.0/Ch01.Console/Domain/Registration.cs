namespace EduHub.Ch01.Domain;

/// <summary>Phiếu đăng ký của Member cho Event (bảng nối M:N).</summary>
public static class RegistrationStatus
{
    public const string Pending = "Pending";
    public const string Confirmed = "Confirmed";
    public const string Cancelled = "Cancelled";
}
public class Registration
{
    // TODO: Guid Id, Guid MemberId, Guid EventId, DateTime RegisteredAt, string Status (private set)
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public string Status { get; private set; }

    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    
    // TODO: hằng số cho Status: "Pending" / "Confirmed" / "Cancelled" (static class hoặc const) — không dùng magic string
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty) throw new ArgumentException("Thiếu MemberId.", nameof(memberId));
        if (eventId == Guid.Empty) throw new ArgumentException("Thiếu EventId.", nameof(eventId));

        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException($"Không thể xác nhận phiếu đang ở trạng thái {Status}.");
        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Phiếu đã được huỷ trước đó.");
        Status = RegistrationStatus.Cancelled;
    }
    
    // TODO: constructor PRIVATE + public static Registration Create(Guid memberId, Guid eventId)
    //       → Status khởi tạo = Pending
    
    // TODO: public void Confirm() — chỉ hợp lệ khi Pending, sai thì throw InvalidOperationException
    // TODO: public void Cancel()  — hợp lệ khi Pending/Confirmed; đã Cancelled thì throw
}

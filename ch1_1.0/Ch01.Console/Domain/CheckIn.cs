namespace EduHub.Ch01.Domain;

public static class CheckInMethod
{
    public const string Manual = "Manual";
    public const string QrCode = "QrCode";
}

/// <summary>Ghi nhận sự hiện diện thực tế của một Registration (quan hệ 1–1).</summary>
public class CheckIn
{
    // TODO: Guid Id, Guid RegistrationId, DateTime CheckedInAt, string Method ("Manual" hoặc "QrCode") (private set)
    public Guid Id { get; private set; }
    public Guid RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }
    public string Method { get; private set; }

    // TODO: constructor PRIVATE + public static CheckIn Create(Registration registration, string method)
    //       → chỉ cho phép khi registration.Status == Confirmed
    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)
    {
        ArgumentNullException.ThrowIfNull(registration);
        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException("Chỉ phiếu đã Confirmed mới được check-in.");
        if (method is not (CheckInMethod.Manual or CheckInMethod.QrCode))
            throw new ArgumentException($"Hình thức không hợp lệ: {method}.", nameof(method));

        return new CheckIn(registration.Id, method);
    }

    // TODO: public string Summary() — chuỗi mô tả để in báo cáo
    public string Summary() => $"Check-in {CheckedInAt:HH:mm:ss} qua {Method} (phiếu {RegistrationId.ToString()[..8]})";
}
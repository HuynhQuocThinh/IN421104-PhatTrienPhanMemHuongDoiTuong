
namespace EduHub.Ch01.Domain;

/// <summary>Ghi nhận sự hiện diện thực tế của một Registration.</summary>
public class CheckIn
{
    public Guid Id { get; private set; }
    public Guid RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }
    public string Method { get; private set; } = string.Empty;

    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    public static CheckIn Create(Registration registration, string method)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (registration.Status != Registration.ConfirmedStatus)
            throw new InvalidOperationException(
                "Chỉ được điểm danh khi đăng ký đã xác nhận.");

        if (method != "Manual" && method != "QrCode")
            throw new ArgumentException(
                "Phương thức phải là Manual hoặc QrCode.", nameof(method));

        return new CheckIn(registration.Id, method);
    }

    public string Summary()
    {
        return $"Registration: {RegistrationId} | Method: {Method} | Time: {CheckedInAt:dd/MM/yyyy HH:mm}";
    }
}

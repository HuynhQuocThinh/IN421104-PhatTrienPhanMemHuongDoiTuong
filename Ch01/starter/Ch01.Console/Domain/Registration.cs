namespace EduHub.Ch01.Domain;

/// <summary>Thông tin đăng ký tham gia giải đấu của sinh viên.</summary>
public class Registration
{
    public const string PendingStatus = "Pending";
    public const string ConfirmedStatus = "Confirmed";
    public const string CancelledStatus = "Cancelled";

    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public string Status { get; private set; } = PendingStatus;

    private Registration(Guid memberId, Guid eventId)
    {
        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.UtcNow;
        Status = PendingStatus;
    }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException(
                "Mã sinh viên không hợp lệ.", nameof(memberId));

        if (eventId == Guid.Empty)
            throw new ArgumentException(
                "Mã giải đấu không hợp lệ.", nameof(eventId));

        return new Registration(memberId, eventId);
    }

    public void Confirm()
    {
        if (Status != PendingStatus)
            throw new InvalidOperationException(
                "Chỉ có thể xác nhận đăng ký đang chờ.");

        Status = ConfirmedStatus;
    }

    public void Cancel()
    {
        if (Status != PendingStatus && Status != ConfirmedStatus)
            throw new InvalidOperationException(
                "Chỉ có thể hủy đăng ký đang chờ hoặc đã xác nhận.");

        Status = CancelledStatus;
    }

    public override string ToString()
    {
        return $"Đăng ký: {MemberId} - Giải đấu: {EventId} - Trạng thái: {Status}";
    }
}

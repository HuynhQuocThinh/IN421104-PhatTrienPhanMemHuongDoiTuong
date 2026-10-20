
namespace EduHub.Ch01.Domain;

/// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
public class Member
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string StudentCode { get; private set; } = string.Empty;
    public DateTime JoinedAt { get; private set; }

    // Constructor private: chỉ tạo đối tượng thông qua Create()
    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
    }

    // Factory method tạo thành viên mới
    public static Member Create(
        string name,
        string email,
        string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được để trống.", nameof(name));

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã sinh viên không được để trống.", nameof(studentCode));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ.", nameof(email));

        return new Member(name, email, studentCode);
    }

    // Behavior: kiểm tra thành viên có đầy đủ thông tin cơ bản không
    public bool IsActive()
    {
        return !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(StudentCode);
    }

    // Hiển thị thông tin thành viên
    public override string ToString()
    {
        return $"{Name} ({StudentCode})";
    }
}

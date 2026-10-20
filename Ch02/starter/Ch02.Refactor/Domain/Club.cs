namespace EduHub.Ch02.Domain;

/// <summary>Câu lạc bộ — chứa quy tắc nghiệp vụ của chính nó, không chứa logic hạ tầng.</summary>
public class Club
{
    public const int MaxMembers = 50;
    public const int MaxNameLength = 100;

    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    /// <summary>Tạo CLB mới, kiểm tra tên hợp lệ (bắt buộc, không quá 100 ký tự).</summary>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name is required");
        if (name.Length > MaxNameLength)
            throw new ArgumentException("Club name too long");

        return new Club { Id = Guid.NewGuid(), Name = name, Description = description };
    }

    /// <summary>CLB còn nhận thêm thành viên không (tối đa 50)?</summary>
    public bool HasRoom(int currentMemberCount) => currentMemberCount < MaxMembers;
}

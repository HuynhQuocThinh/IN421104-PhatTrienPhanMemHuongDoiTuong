
namespace EduHub.Ch01.Domain;

/// <summary>Câu lạc bộ sinh viên.</summary>
public class Club
{
    private readonly List<Member> _members = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    private Club(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Mô tả không được để trống.", nameof(description));

        return new Club(name, description);
    }

    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (_members.Any(m => m.Id == member.Id))
            throw new InvalidOperationException("Thành viên đã có trong câu lạc bộ.");

        _members.Add(member);
    }

    public int MemberCount()
    {
        return _members.Count;
    }
}

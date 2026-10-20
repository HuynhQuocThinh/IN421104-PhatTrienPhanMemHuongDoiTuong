
using EduHub.Ch01.Domain;

Console.WriteLine("=== EduHub - He thong to chuc giai the thao sinh vien ===");

// 1. Tạo đơn vị tổ chức
var club = Club.Create(
    "Tech Club",
    "Don vi to chuc giai the thao sinh vien");

Console.WriteLine($"\nDon vi to chuc: {club.Name}");

// 2. Tạo 3 sinh viên và thêm vào câu lạc bộ
var member1 = Member.Create("Nguyen Van An", "an@example.com", "SV001");
var member2 = Member.Create("Tran Thi Binh", "binh@example.com", "SV002");
var member3 = Member.Create("Le Van Cuong", "cuong@example.com", "SV003");

club.AddMember(member1);
club.AddMember(member2);
club.AddMember(member3);

Console.WriteLine($"So sinh vien: {club.MemberCount()}");

// 3. Tạo giải đấu
var sportsEvent = Event.Create(
    club.Id,
    "Giai bong da sinh vien 2026",
    DateTime.UtcNow.AddDays(7),
    50);

Console.WriteLine($"Giai dau: {sportsEvent.Title}");
Console.WriteLine($"Suc chua: {sportsEvent.Capacity}");

// 4. Đăng ký hai sinh viên
var registrations = new List<Registration>();

if (sportsEvent.HasCapacity(registrations.Count))
{
    var registration1 = Registration.Create(member1.Id, sportsEvent.Id);
    registration1.Confirm();
    registrations.Add(registration1);

    Console.WriteLine($"{member1.Name}: Dang ky thanh cong");
}

if (sportsEvent.HasCapacity(registrations.Count))
{
    var registration2 = Registration.Create(member2.Id, sportsEvent.Id);
    registration2.Confirm();
    registrations.Add(registration2);

    Console.WriteLine($"{member2.Name}: Dang ky thanh cong");
}

// 5. Điểm danh một sinh viên đã được xác nhận
var checkIns = new List<CheckIn>();

var checkIn = CheckIn.Create(registrations[0], "Manual");
checkIns.Add(checkIn);

// 6. Báo cáo
Console.WriteLine("\n=== BAO CAO GIAI DAU ===");
Console.WriteLine($"Ten giai: {sportsEvent.Title}");
Console.WriteLine($"Tong so dang ky: {registrations.Count}");
Console.WriteLine($"Tong so diem danh: {checkIns.Count}");
Console.WriteLine("Danh sach sinh vien:");

foreach (var member in club.Members)
{
    Console.WriteLine($"- {member}");
}

Console.WriteLine("\nChi tiet diem danh:");
foreach (var item in checkIns)
{
    Console.WriteLine(item.Summary());
}

Console.WriteLine("\nDemo complete!");
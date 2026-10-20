using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

// ═══ Composition root: nơi DUY NHẤT dùng "new" cho class cụ thể ═══
IAppLogger logger = new ConsoleLogger();                 // đổi sang FileLogger("club-log.txt") nếu muốn

// Factory: chọn kênh thông báo bằng enum, không new class cụ thể ở nơi khác
INotificationServiceFactory notificationFactory = new NotificationServiceFactory();
INotificationService notifier = notificationFactory.Create(NotificationChannel.Email);

// Decorator: bọc repository gốc để ghi log truy cập
IClubRepository clubRepo = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository memberRepo = new InMemoryMemberRepository();
IEventRepository eventRepo = new InMemoryEventRepository();

IClubService clubService = new ClubService(clubRepo, memberRepo, notifier, logger);
IEventService eventService = new EventService(clubRepo, memberRepo, eventRepo);
IReportService reportService = new ReportService(clubRepo, memberRepo, eventRepo);

// ═══ 1. Dùng service (thay cho ClubManager cũ) ═══
Console.WriteLine("== 1. Tạo CLB và thêm thành viên ==");
var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
var a = clubService.AddMember(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");
var b = clubService.AddMember(club.Id, "Trần Thị B", "b@student.dthu.edu.vn");

// ═══ 2. Observer: đăng ký 2 người nghe, rồi tạo sự kiện ═══
Console.WriteLine("\n== 2. Observer: tạo sự kiện -> 2 observer được báo ==");
eventService.Subscribe(new LogNotifier(logger));
eventService.Subscribe(new EmailNotifier(memberRepo, notifier));
var ev = eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));
eventService.Register(ev.Id, a.Id);
eventService.Register(ev.Id, b.Id);
eventService.CheckIn(ev.Id, a.Id);

// ═══ 3. Strategy: cùng dữ liệu, hai định dạng báo cáo ═══
Console.WriteLine("\n== 3. Strategy: báo cáo Console ==");
IReportStrategy console = new ConsoleReportStrategy();
Console.Write(reportService.Generate(console));

Console.WriteLine("== 3. Strategy: báo cáo CSV ==");
IReportStrategy csv = new CsvReportStrategy();
Console.Write(reportService.Generate(csv));

// ═══ 4. Factory: cùng một lời gọi, đổi kênh chỉ bằng tham số ═══
Console.WriteLine("\n== 4. Factory: tạo kênh thông báo theo yêu cầu ==");
foreach (var channel in Enum.GetValues<NotificationChannel>())
    notificationFactory.Create(channel).Send("a@student.dthu.edu.vn", $"Thử kênh {channel}");

// ═══ 5. Kiểm tra lỗi nghiệp vụ ═══
Console.WriteLine("\n== 5. Kiểm tra validate ==");
try { clubService.CreateClub("", "x", "p@gmail.com"); }
catch (ArgumentException ex) { Console.WriteLine($"  Lỗi đúng dự kiến: {ex.Message}"); }

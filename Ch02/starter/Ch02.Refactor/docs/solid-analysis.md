# Phân tích vi phạm SOLID trong ClubManager

## 1. Single Responsibility Principle (SRP) – Nguyên lý đơn trách nhiệm

**Vị trí:** Dòng 35–64, phương thức `CreateClub()`.

**Vi phạm:** Phương thức vừa kiểm tra dữ liệu đầu vào, vừa tạo và lưu câu lạc bộ, ghi log ra file, đồng thời gửi thông báo cho chủ nhiệm câu lạc bộ.

**Hậu quả:** Khi thay đổi quy tắc kiểm tra dữ liệu, cách lưu trữ hoặc cách gửi thông báo, phải sửa cùng một phương thức. Điều này làm code khó bảo trì và kiểm thử.

**Cách sửa (refactor):**

- Tách xử lý nghiệp vụ câu lạc bộ vào `ClubService`.
- Tách lưu trữ vào `IClubRepository`.
- Tách ghi log vào `IAppLogger`.
- Tách gửi thông báo vào `INotificationService`.

## 2. Open/Closed Principle (OCP) – Nguyên lý đóng/mở

**Vị trí:** Dòng 50–63, trong `CreateClub()`; dòng 81–83, trong `AddMemberToClub()`.

**Vi phạm:** Code sử dụng `if/else` để lựa chọn cách gửi thông báo dựa trên email, đồng thời gọi trực tiếp các thông báo qua `Console.WriteLine()`.

**Hậu quả:** Khi cần thêm hoặc thay đổi kênh thông báo như SMS hay Slack, phải sửa code hiện có. Điều này làm tăng nguy cơ ảnh hưởng đến chức năng đang hoạt động.

**Cách sửa (refactor):**

- Tạo `INotificationService` làm interface chung.
- Sử dụng `NotificationServiceFactory` để tạo dịch vụ thông báo phù hợp.
- Có thể bổ sung dịch vụ mới mà không phải sửa toàn bộ logic nghiệp vụ trong `ClubManager`.

## 3. Dependency Inversion Principle (DIP) – Nguyên lý đảo ngược phụ thuộc

**Vị trí:** Dòng 47–48, 78–79, 94 và 110–111.

**Vi phạm:** `ClubManager` gọi trực tiếp `File.AppendAllText()` để ghi log, phụ thuộc vào cách lưu log cụ thể thay vì một abstraction.

**Hậu quả:** Khó thay đổi cách ghi log sang console hoặc hệ thống logging khác; việc kiểm thử cũng khó khăn vì nghiệp vụ gắn chặt với thao tác file.

**Cách sửa (refactor):**

- Tạo interface `IAppLogger` với phương thức ghi log.
- Tiêm `IAppLogger` vào constructor của lớp cần ghi log.
- Dùng `ConsoleLogger` hoặc `FileLogger` triển khai interface này.
- Có thể dùng `LoggingClubRepository` để bọc `IClubRepository` và ghi log khi truy cập dữ liệu.

## 4. Single Responsibility Principle (SRP) – Trách nhiệm tạo sự kiện bị trộn với thông báo

**Vị trí:** Dòng 88–98, phương thức `CreateEvent()`.

**Vi phạm:** Phương thức vừa tạo sự kiện, vừa ghi log, vừa duyệt danh sách thành viên và gửi thông báo cho từng người.

**Hậu quả:** Khi thay đổi cách thông báo sự kiện, phải sửa code tạo sự kiện. Phương thức cũng khó kiểm thử độc lập.

**Cách sửa (refactor):**

- Chuyển logic tạo sự kiện sang `EventService`.
- Chuyển ghi log sang `IAppLogger`.
- Tạo interface `IEventObserver` cho các đối tượng cần nhận thông báo.
- Sau khi tạo sự kiện, `EventService` thông báo cho các observer như `EmailNotifier` và `LogNotifier`.

## 5. Open/Closed Principle (OCP) – Chức năng báo cáo khó mở rộng

**Vị trí:** Dòng 103–113, phương thức `PrintClubReport()`.

**Vi phạm:** Phương thức vừa tổng hợp số lượng thành viên, vừa định dạng và in báo cáo ra console, đồng thời ghi log. Nếu muốn xuất báo cáo CSV, cần sửa phương thức hiện có.

**Hậu quả:** Chức năng báo cáo phụ thuộc vào một cách xuất dữ liệu, khiến việc mở rộng và bảo trì khó khăn.

**Cách sửa (refactor):**

- Tạo `ReportService` để điều phối việc tạo báo cáo.
- Tạo interface `IReportStrategy`.
- Xây dựng `ConsoleReportStrategy` và `CsvReportStrategy` để hỗ trợ nhiều cách xuất báo cáo mà không phải sửa logic nghiệp vụ.

## 6. Single Responsibility Principle (SRP) – Quản lý dữ liệu và nghiệp vụ tập trung trong một lớp

**Vị trí:** Dòng 30–31 và 116.

**Vi phạm:** `ClubManager` trực tiếp quản lý danh sách câu lạc bộ, danh sách thành viên và cung cấp dữ liệu thông qua `GetAllClubs()`.

**Hậu quả:** Lớp vừa quản lý trạng thái dữ liệu, vừa xử lý nghiệp vụ và các tác vụ phụ trợ. Khi thay đổi cách lưu trữ, cần sửa lớp này. Ngoài ra, trả về trực tiếp danh sách nội bộ có thể khiến bên ngoài thay đổi dữ liệu ngoài dự kiến.

**Cách sửa (refactor):**

- Tạo `IClubRepository` để quản lý lưu trữ và truy vấn câu lạc bộ.
- Tạo `ClubService` để xử lý nghiệp vụ.
- Không trả trực tiếp danh sách nội bộ có thể bị chỉnh sửa; thay vào đó trả về bản sao hoặc kiểu chỉ đọc phù hợp.

## Kết luận

`ClubManager` vi phạm chủ yếu các nguyên lý SRP, OCP và DIP do tập trung nhiều trách nhiệm trong một lớp, phụ thuộc trực tiếp vào thao tác ghi file và khó mở rộng các chức năng thông báo, báo cáo.

Việc refactor cần tách trách nhiệm thành các lớp nhỏ hơn, sử dụng interface để giảm phụ thuộc và áp dụng các Design Pattern phù hợp. Mục tiêu là thay đổi cấu trúc code nhưng vẫn giữ nguyên chức năng của chương trình.

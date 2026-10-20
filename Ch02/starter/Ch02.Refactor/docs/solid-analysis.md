# Phân tích vi phạm SOLID trong ClubManager

## 1. Single Responsibility Principle (SRP)
- Vị trí: các phương thức tạo câu lạc bộ, quản lý thành viên, tạo sự kiện và in báo cáo trong `ClubManager`.
- Vi phạm: một lớp đảm nhận nhiều trách nhiệm khác nhau.
- Hậu quả: khi thay đổi một chức năng, có thể ảnh hưởng đến các chức năng khác.

## 2. Open/Closed Principle (OCP)
- Vị trí: đoạn điều kiện `if/else` lựa chọn kênh thông báo.
- Vi phạm: phải sửa code hiện có mỗi khi thêm một kênh thông báo mới.
- Hậu quả: tăng nguy cơ phát sinh lỗi khi mở rộng hệ thống.

## 3. Liskov Substitution Principle (LSP)
- Cần kiểm tra: các lớp triển khai interface có giữ đúng hành vi mà interface quy định hay không.
- Vi phạm cụ thể: cần xác định từ code thực tế của `ClubManager` và các lớp liên quan.

## 4. Interface Segregation Principle (ISP)
- Vị trí: cần kiểm tra các interface mà `ClubManager` sử dụng hoặc triển khai.
- Vi phạm: nếu một lớp buộc phải phụ thuộc vào những phương thức không cần thiết.
- Hậu quả: các lớp phụ thuộc vào những chức năng chúng không sử dụng.

## 5. Dependency Inversion Principle (DIP)
- Vị trí: các đoạn khởi tạo trực tiếp lớp lưu trữ, ghi log hoặc gửi thông báo bên trong `ClubManager`.
- Vi phạm: lớp nghiệp vụ phụ thuộc trực tiếp vào lớp triển khai cụ thể.
- Hậu quả: khó thay thế thành phần, kiểm thử và mở rộng.

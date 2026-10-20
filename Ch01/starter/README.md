# Ch01 — Phân tích yêu cầu và Domain Modeling EduHub (starter)

Bài tập chương 1 của IN4211, chấm theo HĐ1.3 (bài tập chương). Liên quan CLO1, CLO4, CLO5.
Đề bài đầy đủ và rubric: `../de-bai.html`, `../rubric.html`.

## Cần làm

1. `docs/user-stories.md`: viết ít nhất 5 User Story, mỗi story có ít nhất 2 tiêu chí nghiệm thu, đủ 3 vai trò.
2. `docs/class-diagram.md`: vẽ sơ đồ lớp sơ bộ của 5 class (thuộc tính, khoá ngoại, bội số quan hệ).
3. `Ch01.Console/Domain/`: hoàn thiện 5 class `Member`, `Club`, `Event`, `Registration`, `CheckIn`
   theo các đoạn `// TODO` (private set, constructor private, factory `Create(...)`, ít nhất một method hành vi).
4. `Ch01.Console/Program.cs`: viết demo luồng tạo CLB, thêm thành viên, tạo sự kiện, đăng ký, xác nhận, check-in, báo cáo tham dự.
5. Nộp bài bằng Pull Request (xem mục Nộp bài).

Starter là bộ khung tuỳ chọn: bạn có thể tự tạo solution từ đầu với cấu trúc `Domain/` như đề bài.

## Chạy và build

```bash
dotnet build Ch01.Console.sln
dotnet run --project Ch01.Console
```

Starter build được ngay khi chưa làm (các class còn trống). Khi hoàn thành, chương trình phải in:
thông tin thành viên, thông tin CLB, thông tin sự kiện (số đăng ký / sức chứa), thông tin check-in và báo cáo tham dự.

## Nộp bài

```bash
git switch -c ch01/<MSSV>
git add .
git commit -m "feat(domain): add Member and Club"
git push -u origin ch01/<MSSV>
```

Mở Pull Request về `main`, tag giảng viên, mô tả đủ what, why, how to test.
Commit theo Conventional Commits (`type(scope): subject`), ít nhất 3 commit.

## Tài liệu tham khảo

- [C# classes](https://learn.microsoft.com/dotnet/csharp/fundamentals/types/classes)
- [C# properties](https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/properties)
- [C# constructors](https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/constructors)
- [Conventional Commits](https://www.conventionalcommits.org/vi/v1.0.0/)

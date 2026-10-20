using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Một cài đặt cho mọi loại email — thay cho chuỗi if/else Gmail/Yahoo của starter.</summary>

namespace EduHub.Ch02.Notifications
{
    public class EmailNotificationService : INotificationService
    {
        public void Send(string to, string message) =>
            Console.WriteLine($"  [EMAIL] to {to}: {message}");
    }
}

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Cài đặt Factory. Code gọi Create(kênh) và nhận về INotificationService,
/// không cần biết (và không cần new) class cụ thể. Thêm kênh mới = thêm một nhánh ở đây.
/// </summary>

namespace EduHub.Ch02.Notifications
{
    public class NotificationServiceFactory : INotificationServiceFactory
    {
        public INotificationService Create(NotificationChannel channel) => channel switch
        {
            NotificationChannel.Email => new EmailNotificationService(),
            NotificationChannel.Sms => new SmsNotificationService(),
            NotificationChannel.Slack => new SlackNotificationService(),
            _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, "Unsupported channel")
        };
    }
}

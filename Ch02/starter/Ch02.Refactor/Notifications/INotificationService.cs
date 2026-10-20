using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Gửi thông báo. Thêm kênh mới (SMS, Slack) = thêm class, không sửa code cũ (OCP).</summary>

namespace EduHub.Ch02.Notifications
{
    public interface INotificationService
    {
        void Send(string to, string message);
    }
}

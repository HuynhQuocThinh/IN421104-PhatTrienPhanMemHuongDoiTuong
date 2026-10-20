using System;
using System.Collections.Generic;
using System.Text;

/// <summary>FACTORY: nơi duy nhất biết cách tạo từng loại INotificationService.</summary>

namespace EduHub.Ch02.Notifications
{
    public interface INotificationServiceFactory
    {
        INotificationService Create(NotificationChannel channel);
    }
}

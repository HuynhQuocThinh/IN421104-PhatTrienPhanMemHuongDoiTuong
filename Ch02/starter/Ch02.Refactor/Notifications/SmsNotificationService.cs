using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Notifications
{
    public class SmsNotificationService : INotificationService
    {
        public void Send(string to, string message) =>
            Console.WriteLine($"  [SMS] to {to}: {message}");
    }
}

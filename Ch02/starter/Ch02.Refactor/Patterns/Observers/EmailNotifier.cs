using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Repositories;

/// <summary>Gửi email cho mọi thành viên CLB khi có sự kiện mới.</summary>

namespace EduHub.Ch02.Patterns.Observers
{
    public class EmailNotifier : IEventObserver
    {
        private readonly IMemberRepository _members;
        private readonly INotificationService _notifier;

        public EmailNotifier(IMemberRepository members, INotificationService notifier)
        {
            _members = members;
            _notifier = notifier;
        }

        public void OnEventCreated(Event ev, Club club)
        {
            foreach (var m in _members.GetByClub(club.Id))
                _notifier.Send(m.Email, $"New event '{ev.Title}' at {club.Name} on {ev.Date:dd/MM/yyyy}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;

/// <summary>OBSERVER: bên lắng nghe khi có sự kiện mới được tạo.</summary>

namespace EduHub.Ch02.Patterns.Observers
{
    public interface IEventObserver
    {
        void OnEventCreated(Event ev, Club club);
    }
}

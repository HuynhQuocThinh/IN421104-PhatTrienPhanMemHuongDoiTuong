using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public interface IEventRepository
    {
        IReadOnlyList<Event> GetByClub(Guid clubId);
        Event? GetById(Guid id);
        void Add(Event ev);
    }
}

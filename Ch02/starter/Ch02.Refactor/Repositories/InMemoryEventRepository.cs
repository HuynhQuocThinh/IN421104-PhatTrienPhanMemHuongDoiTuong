using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _store = new();

        public IReadOnlyList<Event> GetByClub(Guid clubId) =>
            _store.Where(e => e.ClubId == clubId).ToList();
        public Event? GetById(Guid id) => _store.FirstOrDefault(e => e.Id == id);
        public void Add(Event ev) => _store.Add(ev);
    }
}

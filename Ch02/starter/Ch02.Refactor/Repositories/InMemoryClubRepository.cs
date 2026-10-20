using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Repository lưu trong bộ nhớ. Sau này đổi sang DB chỉ cần class mới.</summary>

namespace EduHub.Ch02.Repositories
{
    public class InMemoryClubRepository : IClubRepository
    {
        private readonly List<Club> _store = new();

        public IReadOnlyList<Club> GetAll() => _store.AsReadOnly();
        public Club? GetById(Guid id) => _store.FirstOrDefault(c => c.Id == id);
        public void Add(Club club) => _store.Add(club);
    }
}

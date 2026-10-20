using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public class InMemoryMemberRepository : IMemberRepository
    {
        private readonly List<Member> _store = new();

        public IReadOnlyList<Member> GetByClub(Guid clubId) =>
            _store.Where(m => m.ClubId == clubId).ToList();
        public Member? GetById(Guid id) => _store.FirstOrDefault(m => m.Id == id);
        public void Add(Member member) => _store.Add(member);
    }
}

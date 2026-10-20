using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Repositories
{
    public interface IMemberRepository
    {
        IReadOnlyList<Member> GetByClub(Guid clubId);
        Member? GetById(Guid id);
        void Add(Member member);
    }
}

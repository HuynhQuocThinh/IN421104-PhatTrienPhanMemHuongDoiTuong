using EduHub.Ch02.Domain;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Kho dữ liệu CLB — interface nhỏ (ISP), chỉ việc liên quan CLB.</summary>

namespace EduHub.Ch02.Repositories
{
    public interface IClubRepository
    {
        IReadOnlyList<Club> GetAll();
        Club? GetById(Guid id);
        void Add(Club club);
    }
}

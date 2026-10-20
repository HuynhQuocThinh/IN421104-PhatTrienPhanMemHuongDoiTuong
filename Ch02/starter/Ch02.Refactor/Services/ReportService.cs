using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;

/// <summary>Chỉ thống kê dữ liệu; việc định dạng giao cho IReportStrategy (OCP).</summary>
/// 
namespace EduHub.Ch02.Services
{
    public class ReportService : IReportService
    {
        private readonly IClubRepository _clubs;
        private readonly IMemberRepository _members;
        private readonly IEventRepository _events;

        public ReportService(IClubRepository clubs, IMemberRepository members, IEventRepository events)
        {
            _clubs = clubs;
            _members = members;
            _events = events;
        }

        public string Generate(IReportStrategy strategy)
        {
            var stats = _clubs.GetAll().Select(c =>
            {
                var events = _events.GetByClub(c.Id);
                return new ClubStat(
                    c.Name,
                    _members.GetByClub(c.Id).Count,
                    events.Count,
                    events.Sum(e => e.RegisteredMemberIds.Count),
                    events.Sum(e => e.CheckedInMemberIds.Count));
            }).ToList();

            return strategy.Render(stats);
        }
    }
}

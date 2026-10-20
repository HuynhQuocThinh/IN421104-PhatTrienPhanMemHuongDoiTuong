using System;
using System.Collections.Generic;
using System.Text;

/// <summary>STRATEGY: mỗi định dạng báo cáo là một cách "vẽ" cùng một dữ liệu.</summary>

namespace EduHub.Ch02.Patterns.Strategies
{
    public interface IReportStrategy
    {
        string Name { get; }
        string Render(IReadOnlyList<ClubStat> stats);
    }
}

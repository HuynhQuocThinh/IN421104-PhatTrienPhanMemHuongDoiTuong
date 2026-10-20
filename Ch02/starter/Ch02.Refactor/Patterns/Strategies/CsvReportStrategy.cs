using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Patterns.Strategies
{
    public class CsvReportStrategy : IReportStrategy
    {
        public string Name => "CSV";

        public string Render(IReadOnlyList<ClubStat> stats)
        {
            var sb = new StringBuilder("Club,Members,Events,Registered,CheckedIn\n");
            foreach (var s in stats)
                sb.Append($"\"{s.ClubName}\",{s.Members},{s.Events},{s.Registered},{s.CheckedIn}\n");
            return sb.ToString();
        }
    }

}

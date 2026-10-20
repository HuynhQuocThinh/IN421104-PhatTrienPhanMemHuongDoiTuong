using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Patterns.Strategies
{
    public class ConsoleReportStrategy : IReportStrategy
    {
        public string Name => "Console";

        public string Render(IReadOnlyList<ClubStat> stats)
        {
            var sb = new StringBuilder();
            foreach (var s in stats)
                sb.AppendLine($"Club: {s.ClubName} | Members: {s.Members} | Events: {s.Events} | " +
                              $"Registered: {s.Registered} | Checked-in: {s.CheckedIn}");
            return sb.ToString();
        }
    }
}

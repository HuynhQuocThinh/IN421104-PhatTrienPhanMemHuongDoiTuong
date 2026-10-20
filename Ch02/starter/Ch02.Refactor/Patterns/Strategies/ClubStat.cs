using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Dòng thống kê của một CLB — dữ liệu đầu vào chung cho mọi strategy.</summary>

namespace EduHub.Ch02.Patterns.Strategies
{
    public record ClubStat(string ClubName, int Members, int Events, int Registered, int CheckedIn);
}

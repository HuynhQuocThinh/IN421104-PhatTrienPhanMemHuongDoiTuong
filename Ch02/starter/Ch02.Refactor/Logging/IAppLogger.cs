using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Abstraction cho việc ghi log (thay cho File.AppendAllText trong starter).</summary>

namespace EduHub.Ch02.Logging
{
    public interface IAppLogger
    {
        void Info(string message);
    }
}

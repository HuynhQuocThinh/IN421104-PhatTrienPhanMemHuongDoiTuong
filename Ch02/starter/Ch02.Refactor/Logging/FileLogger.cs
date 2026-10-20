using System;
using System.Collections.Generic;
using System.Text;

/// <summary>Ghi log ra file — đổi từ ConsoleLogger sang đây chỉ cần sửa Program.cs.</summary>

namespace EduHub.Ch02.Logging
{
    public class FileLogger : IAppLogger
    {
        private readonly string _path;
        public FileLogger(string path) => _path = path;

        public void Info(string message) =>
            File.AppendAllText(_path, $"[{DateTime.Now:s}] {message}{Environment.NewLine}");
    }
}

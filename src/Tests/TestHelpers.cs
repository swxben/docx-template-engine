using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tests
{
    public static class TestHelpers
    {
        public static string GetRootPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var rootDir = currentDirectory.Substring(0, currentDirectory.IndexOf(@"\src\Tests\bin\", StringComparison.Ordinal));
            return rootDir;
        }
    }
}

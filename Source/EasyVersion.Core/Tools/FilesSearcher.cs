using System.IO;

namespace EasyVersion.Core
{
    interface IFilesSearcher
    {
        string[] SearchFiles(string dir);
    }

    class AssemblyInfoFilesSearcher : IFilesSearcher
    {
        public string[] SearchFiles(string dir)
        {
            return Directory.GetFiles(dir, "AssemblyInfo.cs", SearchOption.AllDirectories);
        }
    }

    class SimpleVersionFilesSearcher : IFilesSearcher
    {
        public string[] SearchFiles(string dir)
        {
            return Directory.GetFiles(dir, "version", SearchOption.AllDirectories);
        }
    }
}

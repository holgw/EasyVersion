using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyVersion.Core
{
    public class Solution
    {
        public string Directory { get; set; }
        public List<VersionFile> VersionFiles { get; }

        // CTOR
        public Solution(string dir)
        {
            Directory = dir ?? throw new ArgumentNullException(nameof(dir));
            this.VersionFiles = new List<VersionFile>();
        }
    }

    public static partial class Extensions
    {
        public static Version GetMax(this IEnumerable<VersionFile> verFiles)
        {
            return verFiles
                .Select(x => x.Version)
                .OrderByDescending(x => x.Major)
                .ThenByDescending(x => x.Minor)
                .ThenByDescending(x => x.Patch)
                .FirstOrDefault();
        }
    }
}

using System;

namespace EasyVersion.Core
{
    public class VersionFile
    {
        public string Path { get; }
        public Version Version { get; set; }

        // CTOR
        public VersionFile(string path, Version v)
        {
            this.Path = path ?? throw new ArgumentNullException(nameof(path));
            this.Version = v ?? throw new ArgumentNullException(nameof(v));
        }

        // METHODS
        public override string ToString()
        {
            return $"{this.Version.Major}.{this.Version.Minor}.{this.Version.Patch}.0";
        }
    }
}

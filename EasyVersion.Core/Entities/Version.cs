using System;

namespace EasyVersion.Core
{
    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }

        // CTOR
        public Version(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        // METHODS
        public void IncrementMajor()
        {
            this.Major++;
            this.Minor = 0;
            this.Patch = 0;
        }
        public void IncrementMinor()
        {
            this.Minor++;
            this.Patch = 0;
        }
        public void IncrementPatch()
        {
            this.Patch++;
        }
        public void Increment(IncrementType incrementType)
        {
            switch (incrementType)
            {
                case IncrementType.IncrementMajor: this.IncrementMajor(); break;
                case IncrementType.IncrementMinor: this.IncrementMinor(); break;
                case IncrementType.IncrementPatch: this.IncrementPatch(); break;
                default: throw new InvalidOperationException("Unknown increment type");
            }
        }
        public override string ToString() => $"{this.Major}.{this.Minor}.{this.Patch}.0";
    }
}

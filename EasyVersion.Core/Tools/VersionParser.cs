using System;

namespace EasyVersion.Core
{
    public interface IVersionParser
    {
        Version ParseVersion(string content);
    }

    public class VersionParser : IVersionParser
    {
        public Version ParseVersion(string content)
        {
            short major = 0;
            short minor = 0;
            short patch = 0;

            string[] splitStr = content.Split('.');

            if (splitStr.Length >= 1)
            {
                Int16.TryParse(splitStr[0], out major);

                if (splitStr.Length >= 2)
                {
                    Int16.TryParse(splitStr[1], out minor);

                    if (splitStr.Length >= 3)
                    {
                        Int16.TryParse(splitStr[2], out patch);
                    }
                }
            }

            return new Version(major, minor, patch);
        }
    }
}

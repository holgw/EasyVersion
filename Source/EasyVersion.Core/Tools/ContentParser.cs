using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EasyVersion.Core
{
    interface IContentParser
    {
        Version ParseVersion(string content);
        string SetVersion(string content, Version v);
    }

    class SimpleVersionContentParser : IContentParser
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
        public string SetVersion(string content, Version v)
        {
            return v.ToString();
        }
    }

    internal class AssemblyInfoContentParser : IContentParser
    {
        private readonly IVersionParser _versionParser;
        private readonly string _pattern = @"(?<start>^\[assembly: [a-zA-Z_.]*AssemblyFileVersion\("")(?<version>.*)(?<end>""\)]$)";

        // CTOR
        public AssemblyInfoContentParser(IVersionParser versionParser)
        {
            _versionParser = versionParser ?? throw new ArgumentNullException(nameof(versionParser));
        }

        // METHODS
        public Version ParseVersion(string content)
        {
            try
            {
                Version v = null;

                // Разобьем содержимое файла на строки
                var rows = this.GetRows(content);
                var updatedRows = new List<string>();
                foreach (string row in rows)
                {
                    var match = Regex.Match(row, _pattern);
                    string strVersion = match.Groups["version"].Value;                    
                    if (!String.IsNullOrEmpty(strVersion))
                    {
                        v = _versionParser.ParseVersion(strVersion);
                        break;
                    }
                }

                return v;
            }
            catch (Exception ex)
            {
                string errMsg = $"Can't parse version from string";
                throw new Exception(errMsg, ex);
            }
        }
        public string SetVersion(string content, Version v)
        {
            try
            {
                // Разобьем содержимое файла на строки
                var rows = this.GetRows(content);

                var updatedRows = new List<string>();
                foreach (string row in rows)
                {
                    var match = Regex.Match(row, _pattern);
                    string strVersion = match.Groups["version"].Value;
                    var replacePattern = @"${start}" + v.ToString() + "${end}";
                    var updatedRow = Regex.Replace(row, _pattern, replacePattern);
                    updatedRows.Add(updatedRow);
                }

                return this.CombineRows(updatedRows);
            }
            catch (Exception ex)
            {
                string errMsg = $"Can't update version from string";
                throw new Exception(errMsg, ex);
            }
        }

        // METHODS: Private
        string[] GetRows(string content)
        {
            return content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Select(x => x.Trim())
                .ToArray();
        }
        string CombineRows(IEnumerable<string> rows)
        {
            return String.Join(Environment.NewLine, rows);
        }
    }
}

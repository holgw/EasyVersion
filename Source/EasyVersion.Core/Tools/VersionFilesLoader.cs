using System;
using System.Collections.Generic;

namespace EasyVersion.Core
{
    interface IVersionFilesLoader
    {
        VersionFile[] LoadFiles(string projectDir);
    }

    class VersionFilesLoader : IVersionFilesLoader
    {
        private readonly IContentParser _contentParser;
        private readonly ITextFileEditor _textFileEditor;
        private readonly IFilesSearcher _filesSearcher;

        // CTOR
        public VersionFilesLoader(IContentParser contentParser, ITextFileEditor textFileEditor, IFilesSearcher filesSearcher)
        {
            _contentParser = contentParser ?? throw new ArgumentNullException(nameof(contentParser));
            _textFileEditor = textFileEditor ?? throw new ArgumentNullException(nameof(textFileEditor));
            _filesSearcher = filesSearcher ?? throw new ArgumentNullException(nameof(filesSearcher));
        }

        // METHODS
        public VersionFile[] LoadFiles(string dir)
        {
            var ret = new List<VersionFile>();

            var pathList = _filesSearcher.SearchFiles(dir);
            foreach (var path in pathList)
            {
                string content = _textFileEditor.ReadFile(path);
                var v = _contentParser.ParseVersion(content);
                
                if (v == null)
                {
                    Console.WriteLine($"Can't parse version file content: {path}");
                    continue;
                }

                var prjVersionFile = new VersionFile(path, v);
                ret.Add(prjVersionFile);
            }

            return ret.ToArray();
        }
    }
}

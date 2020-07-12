using System;
using System.Collections.Generic;

namespace EasyVersion.Core
{
    public interface IVersionsManager
    {
        string[] IncrementVersions(string projectDir, IncrementType incrementType);
    }

    class VersionsManager : IVersionsManager
    {
        private readonly ITextFileEditor _textFileEditor;
        private readonly IContentParser _contentParser;
        private readonly IVersionFilesLoader _filesLoader;

        // CTOR
        public VersionsManager(ITextFileEditor textFileEditor, IContentParser contentParser, IVersionFilesLoader filesLoader)
        {
            _textFileEditor = textFileEditor ?? throw new ArgumentNullException(nameof(textFileEditor));
            _contentParser = contentParser ?? throw new ArgumentNullException(nameof(contentParser));
            _filesLoader = filesLoader ?? throw new ArgumentNullException(nameof(filesLoader));
        }

        // METHODS
        public string[] IncrementVersions(string projectDir, IncrementType incrementType)
        {
            var processedFilesList = new List<string>(); // Перечень обработанных файлов

            var files = _filesLoader.LoadFiles(projectDir);
            Console.WriteLine($"Files detected: {files.Length}");

            foreach (var file in files)
            {
                file.Version.Increment(incrementType);
                string oldContent = _textFileEditor.ReadFile(file.Path);
                string newContent = _contentParser.SetVersion(oldContent, file.Version);

                if (oldContent != newContent)
                { 
                    _textFileEditor.WriteFile(file.Path, newContent);
                    Console.WriteLine($"    {file.Path} : {file.Version} [SUCCESS]");
                    processedFilesList.Add(file.Path);
                }
                else
                {
                    Console.WriteLine($"    {file.Path} : {file.Version} [FAIL]");
                }
            }

            return processedFilesList.ToArray();
        }
    }

    public static class Factory
    {
        public static class SimpleVersion
        {
            public static IVersionsManager GetManager()
            {
                var textFileEditor = new TextFileEditor();
                var contentParser = new SimpleVersionContentParser();
                var filesSearcher = new SimpleVersionFilesSearcher();
                var filesLoader = new VersionFilesLoader(contentParser, textFileEditor, filesSearcher);
                return new VersionsManager(textFileEditor, contentParser, filesLoader);
            }
        }

        public static class AssemblyInfo
        {
            public static IVersionsManager GetManager()
            {
                var textFileEditor = new TextFileEditor();
                var versionParser = new VersionParser();
                var contentParser = new AssemblyInfoContentParser(versionParser);
                var filesSearcher = new AssemblyInfoFilesSearcher();
                var filesLoader = new VersionFilesLoader(contentParser, textFileEditor, filesSearcher);
                return new VersionsManager(textFileEditor, contentParser, filesLoader);
            }
        }
    }
}

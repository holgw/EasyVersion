using System;
using System.IO;

namespace EasyVersion.Core
{
    interface ITextFileEditor
    {
        string ReadFile(string filePath);
        void WriteFile(string filePath, string newContent);
    }

    class TextFileEditor : ITextFileEditor
    {
        // METHODS
        public string ReadFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                string errMsg = $"Can't read text file [File path: {filePath}]";
                throw new Exception(errMsg, ex);
            }
        }
        public void WriteFile(string filePath, string newContent)
        {
            try
            {
                File.WriteAllText(filePath, newContent);
            }
            catch (Exception ex)
            {
                string errMsg = $"Can't write text file [File path: {filePath}]";
                throw new Exception(errMsg, ex);
            }
        }
    }
}

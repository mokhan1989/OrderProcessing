using OrderProcessingConsoleApp.Interfaces;
using System;
using System.IO;

namespace OrderProcessingConsoleApp.Services
{
    public class DirectoryService : IDirectoryService
    {
        public string GetFilePath(string fileName)
        {
            var currentDir = Environment.CurrentDirectory;
            var fullPath = Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\Data\" + fileName + ""));

            return fullPath;
        }

        public string ReadTextFileFromPath(string filePath)
        {
            var text = "";

            try
            {
                text = File.ReadAllText(filePath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Could not read from path: {filePath} with message {ex.Message}");
            }

            return text;
        }
    }
}

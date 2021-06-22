namespace OrderProcessingConsoleApp.Interfaces
{
    public interface IDirectoryService
    {
        string GetFilePath(string fileName);

        string ReadTextFileFromPath(string filePath);
    }
}

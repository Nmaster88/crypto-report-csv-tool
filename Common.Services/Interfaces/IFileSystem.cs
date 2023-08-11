namespace Common.Services.Interfaces
{
    public interface IFileSystem
    {
        bool FileExists(string filePath);
        void CreateEmptyFile(string filePath);
        void DeleteFile(string filePath);
        void WriteAllText(string filePath, string text);
        string ReadAllText(string filePath);
    }
}

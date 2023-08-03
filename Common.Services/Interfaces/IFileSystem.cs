namespace Common.Services.Interfaces
{
    public interface IFileSystem
    {
        bool FileExists(string filePath);
        void CreateEmptyFile(string filePath);
        void Delete(string filePath);
    }
}

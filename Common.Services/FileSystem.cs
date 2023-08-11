using Common.Services.Interfaces;

namespace Common.Services
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void CreateEmptyFile(string filePath)
        {
            File.Create(filePath).Close();
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public void WriteAllText(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
        }

        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}

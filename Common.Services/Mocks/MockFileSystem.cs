using Common.Services.Interfaces;

namespace Common.Services.Mocks
{
    public class MockFileSystem : IFileSystem
    {
        private readonly Dictionary<string, string> fileExistenceMap = new Dictionary<string, string>();

        public bool FileExists(string filePath)
        {
            return fileExistenceMap.ContainsKey(filePath);
        }

        public void CreateEmptyFile(string filePath)
        {
            fileExistenceMap[filePath] = "";
        }

        public void DeleteFile(string filePath)
        {
            fileExistenceMap.Clear();
        }

        public void WriteAllText(string filePath, string text)
        {
            fileExistenceMap[filePath] = text;
        }

        public string ReadAllText(string filePath)
        {
            return fileExistenceMap[filePath];
        }
    }
}

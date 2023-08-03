using Common.Services.Interfaces;

namespace Common.Services.Mocks
{
    public class MockFileSystem : IFileSystem
    {
        private readonly Dictionary<string, bool> fileExistenceMap = new Dictionary<string, bool>();

        public bool FileExists(string filePath)
        {
            return fileExistenceMap.ContainsKey(filePath) && fileExistenceMap[filePath];
        }

        public void CreateEmptyFile(string filePath)
        {
            fileExistenceMap[filePath] = true;
        }

        public void Delete(string filePath)
        {
            fileExistenceMap.Clear();
        }
    }
}

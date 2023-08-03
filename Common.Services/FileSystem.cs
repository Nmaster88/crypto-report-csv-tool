﻿using Common.Services.Interfaces;

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

        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }
    }
}

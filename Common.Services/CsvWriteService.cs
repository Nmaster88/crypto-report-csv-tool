using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvWriterService : Interfaces.IWriter
    {
        private StreamReader? _streamReader;
        private CsvReader? _csvReader;
        private readonly IFileSystem _fileSystem;

        public CsvWriterService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Open(string filePath)
        {
            if (!_fileSystem.FileExists(filePath))
            {
                _fileSystem.CreateEmptyFile(filePath);
            }

            _streamReader = new StreamReader(filePath) ?? throw new ArgumentNullException(filePath);
            _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);
        }

        public void Close()
        {
            _csvReader?.Dispose();
            _streamReader?.Dispose();
        }
        public void Dispose()
        {
            Close();
        }

        public void WriteRecords<T>(List<T> list)
        {
            throw new NotImplementedException();
        }
    }

}
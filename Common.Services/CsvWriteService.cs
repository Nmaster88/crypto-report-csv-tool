using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvWriterService : Interfaces.IWriter
    {
        private IStreamReaderWrapper? _streamReader;
        private CsvReader? _csvReader;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamReaderWrapperFactory _streamReaderWrapperFactory;

        public CsvWriterService(
            IFileSystem fileSystem, 
            IStreamReaderWrapperFactory streamReaderWrapperFactory
            )
        {
            _fileSystem = fileSystem;
            _streamReaderWrapperFactory = streamReaderWrapperFactory;
        }

        public void Open(string filePath)
        {
            if (!_fileSystem.FileExists(filePath))
            {
                _fileSystem.CreateEmptyFile(filePath);
            }

            _streamReader = _streamReaderWrapperFactory.Create(filePath) ?? throw new ArgumentNullException(filePath);
            _csvReader = new CsvReader(new StreamReaderWrapperAdapter(_streamReader), CultureInfo.InvariantCulture);
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
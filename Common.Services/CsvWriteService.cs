using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace Common.Services
{
    /// <summary>
    /// Overcomplicated class design
    /// </summary>
    public class CsvWriterService : Interfaces.IWriter
    {
        private readonly IStreamReaderWrapper? _streamReader;
        private readonly CsvReader? _csvReader;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamReaderWrapperFactory _streamReaderWrapperFactory;

        public CsvWriterService(
            IFileSystem fileSystem,
            IStreamReaderWrapperFactory streamReaderWrapperFactory,
            string filePath
            )
        {
            _fileSystem = fileSystem;
            _streamReaderWrapperFactory = streamReaderWrapperFactory;
            _streamReader = _streamReaderWrapperFactory.Create(filePath) ?? throw new ArgumentNullException(filePath);
            _csvReader = new CsvReader(new StreamReaderWrapperAdapter(_streamReader), CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }

        public void WriteRecords<T>(List<T> list)
        {
            throw new NotImplementedException();
        }
    }

}
using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvWriterService : Interfaces.IWriter
    {
        private readonly TextWriter? _textWriter;
        private readonly CsvWriter? _csvWriter;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamWriterWrapperFactory _streamWriterWrapperFactory;

        public CsvWriterService(
            IFileSystem fileSystem,
            IStreamWriterWrapperFactory streamWriterWrapperFactory,
            string filePath
            )
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            _streamWriterWrapperFactory = streamWriterWrapperFactory ?? throw new ArgumentNullException(nameof(streamWriterWrapperFactory));

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            var _streamWriter = _streamWriterWrapperFactory.Create(filePath) ?? throw new ArgumentNullException(filePath);
            _textWriter = new StreamWriterWrapperAdapter(_streamWriter);
            _csvWriter = new CsvWriter(_textWriter, CultureInfo.InvariantCulture);

            if (!_fileSystem.FileExists(filePath))
            {
                _fileSystem.CreateEmptyFile(filePath);
            }
        }

        public void Dispose()
        {
            _csvWriter?.Dispose();
            _textWriter?.Dispose();
        }

        public void WriteRecords<T>(List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            _csvWriter?.WriteRecords(list);
        }
    }

}
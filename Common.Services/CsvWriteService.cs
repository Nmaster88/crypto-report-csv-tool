using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvWriterService : Interfaces.IWriter
    {
        private readonly string _filePath;
        private readonly TextWriter? _textWriter;
        private readonly CsvWriter? _csvWriter;
        private readonly IStreamWriterWrapperFactory _streamWriterWrapperFactory;

        public CsvWriterService(
            IStreamWriterWrapperFactory streamWriterWrapperFactory,
            string filePath
            )
        {
            _streamWriterWrapperFactory = streamWriterWrapperFactory ?? throw new ArgumentNullException(nameof(streamWriterWrapperFactory));

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            _filePath = filePath;

            var _streamWriter = _streamWriterWrapperFactory.Create(_filePath) ?? throw new ArgumentNullException(_filePath);
            _textWriter = new StreamWriterWrapperAdapter(_streamWriter);
            _csvWriter = new CsvWriter(_textWriter, CultureInfo.InvariantCulture);


        }

        public void Dispose()
        {
            _csvWriter?.Flush();
            _csvWriter?.Dispose();
            _textWriter?.Dispose();
        }

        public void WriteRecords<T>(List<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            //TODO: the file is created on disk, but write records doesn't seem to be creating values inside the file
            _csvWriter?.WriteRecords(list);
            
        }
    }

}
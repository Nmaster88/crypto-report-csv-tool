using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace Common.Services
{
    public class CsvReaderService : Interfaces.IReader
    {
        private readonly TextReader? _textReader;
        private readonly CsvReader? _csvReader;

        public CsvReaderService(IStreamReaderWrapper reader)
        {
            _ = reader ?? throw new ArgumentNullException(nameof(reader));
            _textReader = new StreamReaderWrapperAdapter(reader);
            _csvReader = new CsvReader(_textReader, CultureInfo.InvariantCulture);
        }

        public List<T> ReadRecords<T>()
        {
            List<T>? records = null;

            records = _csvReader?.GetRecords<T>().ToList();

            return records;
        }
        public void Close()
        {
            _csvReader?.Dispose();
            _textReader?.Dispose();
        }
        public void Dispose()
        {
            _csvReader?.Dispose();
            _textReader?.Dispose();
        }

        public void Open(string filePath)
        {
            throw new NotImplementedException();
        }
    }

}
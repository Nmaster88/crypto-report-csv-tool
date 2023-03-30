using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvReaderService : IReader
    {
        private StreamReader _streamReader;
        private CsvReader _csvReader;

        public void Open(string filePath)
        {
            _streamReader = new StreamReader(filePath) ?? throw new ArgumentNullException(filePath);
            _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);
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
            _streamReader?.Dispose();
        }
        public void Dispose()
        {
            Close();
        }
    }

}
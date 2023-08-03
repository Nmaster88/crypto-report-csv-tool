using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvReaderService : Interfaces.IReader
    {
        private StreamReader _streamReader;
        private CsvReader _csvReader;

        public void Open(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

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
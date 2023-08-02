using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvWriteService : IWriter, IDisposable
    {
        private StreamReader _streamReader;
        private CsvReader _csvReader;

        public void Open(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
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
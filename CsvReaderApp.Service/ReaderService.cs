using Common.Services.Interfaces;

namespace CsvReaderApp.Services
{
    public interface IReaderService
    {
        List<T> ReadRecords<T>(string filePath);
    }
    public class ReaderService : IReaderService
    {
        private readonly IReader _reader;

        public ReaderService(IReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public List<T> ReadRecords<T>(string filePath)
        {
            _ = filePath ?? throw new ArgumentNullException(filePath);

            _reader.Open(filePath);
            List<T>? records = _reader.ReadRecords<T>();

            return records;
        }
    }
}
using Common.Services.Interfaces;

namespace CsvReaderApp.Services
{
    public interface IWriteService
    {
        List<T> WriteRecords<T>(string filePath);
    }
    public class WriteService : IWriteService
    {
        private readonly IReader _reader;

        public WriteService(IReader reader)
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

        public List<T> WriteRecords<T>(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
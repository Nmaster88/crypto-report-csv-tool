using Common.Services;

namespace CsvReaderApp.Services
{
    public interface IReaderService
    {
        List<T> ReadRecords<T>();
    }
    public class ReaderService : IReaderService
    {
        private readonly IReader _reader;

        public ReaderService(IReader reader)
        {
        }

        public string FilePath { get; }

        public ReaderService(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(filePath);
        }

        public List<T> ReadRecords<T>()
        {
            List<T>? records = _reader.ReadRecords<T>();

            return records;
        }
    }
}
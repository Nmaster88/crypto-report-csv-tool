using CsvHelper;
using System.Globalization;

namespace CsvReaderApp.Services
{
    public interface IReaderService
    {
        List<T> ReadRecords<T>();
    }
    public class ReaderService : IReaderService
    {
        public string FilePath { get; }

        public ReaderService(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(filePath);
        }

        public List<T> ReadRecords<T>()
        {
            List<T>? records = null;
            using (var reader = new StreamReader(FilePath))
            using (var Csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = Csv.GetRecords<T>().ToList();
            }

            return records;
        }
    }
}
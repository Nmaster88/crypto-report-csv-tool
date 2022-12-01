using CsvHelper;
using CsvHelper.Configuration;
using CsvReaderApp.Models;
using System.Data;
using System.Globalization;

namespace CsvReaderApp.Services
{
    public class ReaderService
    {
        public string FilePath { get; }

        public ReaderService(string filePath)
        {
            FilePath = filePath ?? throw new ArgumentNullException(filePath);
        }

        public IEnumerable<T> Read<T>()
        {
            IEnumerable<T>? records = null;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            using (var reader = new StreamReader(FilePath))
            using (var Csv = new CsvReader(reader, config))
            {
                records = Csv.GetRecords<T>();
            }

            return records;
        }
    }
}
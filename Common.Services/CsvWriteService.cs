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

        public CsvWriterService(
            string filePath
            )
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            _filePath = filePath;
            _textWriter = new StreamWriter(_filePath);

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
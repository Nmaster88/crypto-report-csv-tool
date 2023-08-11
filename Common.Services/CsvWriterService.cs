using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    public class CsvWriterService : Interfaces.IWriter
    {
        private readonly TextWriter? _textWriter;
        private readonly CsvWriter? _csvWriter;

        public CsvWriterService(
            IStreamWriterWrapper writer
            )
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            _textWriter = new StreamWriterWrapperAdapter(writer);
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
            _csvWriter?.WriteRecords(list);
        }
    }

}
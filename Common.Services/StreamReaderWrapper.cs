using Common.Services.Interfaces;
using System.IO;
using System.Text;

namespace Common.Services
{
    public class StreamReaderWrapper : TextReader, IStreamReaderWrapper
    {
        private StreamReader? _streamReader;

        public StreamReaderWrapper(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            _streamReader = new StreamReader(filePath);
        }

        public string ReadLine()
        {
            if (_streamReader == null)
            {
                throw new Exception();
            }
            return _streamReader?.ReadLine();
        }

        public override int Read(char[] buffer, int offset, int count)
        {
            return _streamReader?.Read(buffer, offset, count) ?? 0;
        }

        public override async Task<int> ReadAsync(char[] buffer, int offset, int count)
        {
            return await (_streamReader?.ReadAsync(buffer, offset, count) ?? Task.FromResult(0));
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }

        public void Open(string filePath)
        {
            _streamReader = new StreamReader(filePath);
        }
    }
}

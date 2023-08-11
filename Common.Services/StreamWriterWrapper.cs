using Common.Services.Interfaces;
using System.Text;

namespace Common.Services
{
    public class StreamWriterWrapper : TextWriter, IStreamWriterWrapper
    {
        private StreamWriter? _streamWriter;

        public StreamWriterWrapper(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            _streamWriter = new StreamWriter(filePath);
        }

        public override Encoding Encoding => _streamWriter?.Encoding ?? Encoding.Default;

        public Encoding GetEncoding()
        {
            return this.Encoding;
        }

        public override void Flush()
        {
            _streamWriter?.Flush();
        }

        public override async Task FlushAsync()
        {
            _streamWriter?.FlushAsync().ConfigureAwait(false);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _streamWriter?.Write(buffer, index, count);
        }

        public override async Task WriteAsync(char[] buffer, int index, int count)
        {
            _streamWriter?.WriteAsync(buffer, index, count).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }
    }
}

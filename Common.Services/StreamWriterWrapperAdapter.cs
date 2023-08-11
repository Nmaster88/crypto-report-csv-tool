using Common.Services.Interfaces;
using System.IO;
using System.Text;

namespace Common.Services
{
    public class StreamWriterWrapperAdapter : TextWriter
    {
        private readonly IStreamWriterWrapper _streamWriterWrapper;

        public StreamWriterWrapperAdapter(IStreamWriterWrapper streamWriterWrapper)
        {
            _streamWriterWrapper = streamWriterWrapper;
        }

        public override Encoding Encoding => _streamWriterWrapper.GetEncoding();

        public override void WriteLine(string text) => _streamWriterWrapper.WriteLine(text);

        public override void Flush()
        {
            _streamWriterWrapper?.Flush();
        }

        public override async Task FlushAsync()
        {
            _streamWriterWrapper?.FlushAsync().ConfigureAwait(false);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _streamWriterWrapper?.Write(buffer, index, count);
        }

        public override async Task WriteAsync(char[] buffer, int index, int count)
        {
            await _streamWriterWrapper?.WriteAsync(buffer, index, count);
        }

        public void Dispose()
        {
            _streamWriterWrapper?.Dispose();
        }
    }
}

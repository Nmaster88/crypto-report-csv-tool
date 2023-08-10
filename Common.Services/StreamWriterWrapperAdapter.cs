using Common.Services.Interfaces;
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
    }
}

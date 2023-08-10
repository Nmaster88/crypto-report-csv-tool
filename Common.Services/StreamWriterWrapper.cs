using Common.Services.Interfaces;
using System.Text;

namespace Common.Services
{
    public class StreamWriterWrapper : TextWriter, IStreamWriterWrapper
    {
        private StreamWriter? _streamWriter;

        public StreamWriterWrapper(string filePath)
        {
            _streamWriter = new StreamWriter(filePath);
        }

        public override Encoding Encoding => _streamWriter?.Encoding ?? Encoding.Default;

        public Encoding GetEncoding()
        {
            return this.Encoding;
        }
    }
}

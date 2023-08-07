using Common.Services.Interfaces;

namespace Common.Services
{
    public class StreamReaderWrapper : TextReader, IStreamReaderWrapper
    {
        private StreamReader? _streamReader;

        public StreamReaderWrapper(string filePath)
        {
            _streamReader = new StreamReader(filePath);
        }

        public string ReadLine()
        {
            if(_streamReader == null)
            {
                throw new Exception();
            }
            return _streamReader?.ReadLine();
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

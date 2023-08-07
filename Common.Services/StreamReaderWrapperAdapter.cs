using Common.Services.Interfaces;

namespace Common.Services
{
    public class StreamReaderWrapperAdapter : TextReader, IDisposable
    {
        private readonly IStreamReaderWrapper _streamReaderWrapper;

        public StreamReaderWrapperAdapter(IStreamReaderWrapper streamReaderWrapper)
        {
            _streamReaderWrapper = streamReaderWrapper;
        }

        public void Open(string filePath)
        {
            _streamReaderWrapper.Open(filePath);
        }

        public override string? ReadLine() => _streamReaderWrapper.ReadLine();

        public void Dispose()
        {
            _streamReaderWrapper.Dispose();
        }
    }
}

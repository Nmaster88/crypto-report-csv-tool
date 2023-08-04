using Common.Services.Interfaces;

namespace Common.Services
{
    public class StreamReaderWrapperAdapter : TextReader
    {
        private readonly IStreamReaderWrapper _streamReaderWrapper;

        public StreamReaderWrapperAdapter(IStreamReaderWrapper streamReaderWrapper)
        {
            _streamReaderWrapper = streamReaderWrapper;
        }

        public override string? ReadLine() => _streamReaderWrapper.ReadLine();
    }
}

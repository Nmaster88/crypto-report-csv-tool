using Common.Services.Interfaces;

namespace Common.Services
{
    public interface IStreamReaderWrapperFactory
    {
        IStreamReaderWrapper Create(string filePath);
    }

    public class StreamReaderWrapperFactory : IStreamReaderWrapperFactory
    {
        public IStreamReaderWrapper Create(string filePath)
        {
            return new StreamReaderWrapper(filePath);
        }
    }
}

using Common.Services.Interfaces;

namespace Common.Services
{
    public interface IStreamWriterWrapperFactory
    {
        IStreamWriterWrapper Create(string filePath);
    }

    public class StreamWriterWrapperFactory : IStreamWriterWrapperFactory
    {
        public IStreamWriterWrapper Create(string filePath)
        {
            return new StreamWriterWrapper(filePath);
        }
    }
}

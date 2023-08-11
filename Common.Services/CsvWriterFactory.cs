using Common.Services.Interfaces;

namespace Common.Services
{
    public class CsvWriterFactory : ICsvWriterFactory
    {
        private readonly IStreamWriterWrapperFactory _writerFactory;
        public CsvWriterFactory(IStreamWriterWrapperFactory writerFactory)
        {
            _writerFactory = writerFactory;
        }
        public IWriter Create(string filePath)
        {
            return new CsvWriterService(_writerFactory.Create(filePath));

        }
    }
}

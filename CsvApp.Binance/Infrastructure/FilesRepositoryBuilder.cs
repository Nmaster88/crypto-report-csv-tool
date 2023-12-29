using Common.Services.Interfaces;

namespace CsvApp.Binance.Infrastructure
{
    public class FilesRepositoryBuilder
    {
        private readonly IReader _reader;
        public FilesRepositoryBuilder(IReader reader)
        {
            _reader = reader;
        }

        public FilesRepository Build(string filePath)
        {
            var fileRepository = new FilesRepository(_reader);

            fileRepository.FilePath = filePath;

            return fileRepository;
        }
    }
}

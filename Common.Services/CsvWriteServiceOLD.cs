using Common.Services.Interfaces;
using CsvHelper;
using System.Globalization;

namespace Common.Services
{
    /// <summary>
    /// Overcomplicated class design
    /// </summary>
    public class CsvWriterServiceOLD : Interfaces.IWriterOLD
    {
        private IStreamReaderWrapper? _streamReader;
        private CsvReader? _csvReader;
        private bool disposed = false;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamReaderWrapperFactory _streamReaderWrapperFactory;

        public CsvWriterServiceOLD(
            IFileSystem fileSystem, 
            IStreamReaderWrapperFactory streamReaderWrapperFactory
            )
        {
            _fileSystem = fileSystem;
            _streamReaderWrapperFactory = streamReaderWrapperFactory;
        }

        public void Open(string filePath)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(CsvReaderService), "Cannot call Open on a disposed object.");
            }

            if(_streamReader != null || _csvReader != null) 
            {
                throw new InvalidOperationException("Call Close method first. It is in Opened state already.");
            }

            if(filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!_fileSystem.FileExists(filePath))
            {
                _fileSystem.CreateEmptyFile(filePath);
            }

            _streamReader = _streamReaderWrapperFactory.Create(filePath) ?? throw new ArgumentNullException(filePath);
            _csvReader = new CsvReader(new StreamReaderWrapperAdapter(_streamReader), CultureInfo.InvariantCulture);
        }

        public void Close()
        {
            _csvReader?.Dispose();
            _streamReader?.Dispose();
            _csvReader = null;
            _streamReader = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
            {
                return;
            }

            if (!disposed)
            {
                if (disposing)
                {
                    Close();
                }

                // Release unmanaged resources (if any) and set large fields to null
                // ...

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void WriteRecords<T>(List<T> list)
        {
            throw new NotImplementedException();
        }
    }

}
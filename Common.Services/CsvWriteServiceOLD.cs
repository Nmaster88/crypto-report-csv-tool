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
        private IStreamWriterWrapper? _streamWriter;
        private CsvWriter? _csvWriter;
        private bool disposed = false;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamWriterWrapperFactory _streamWriterWrapperFactory;

        public CsvWriterServiceOLD(
            IFileSystem fileSystem,
            IStreamWriterWrapperFactory streamWriterWrapperFactory
            )
        {
            _fileSystem = fileSystem;
            _streamWriterWrapperFactory = streamWriterWrapperFactory;
        }

        public void Open(string filePath)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(CsvReaderServiceOLD), "Cannot call Open on a disposed object.");
            }

            if (_streamWriter != null || _csvWriter != null)
            {
                throw new InvalidOperationException("Call Close method first. It is in Opened state already.");
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!_fileSystem.FileExists(filePath))
            {
                _fileSystem.CreateEmptyFile(filePath);
            }

            _streamWriter = _streamWriterWrapperFactory.Create(filePath) ?? throw new ArgumentNullException(filePath);
            _csvWriter = new CsvWriter(new StreamWriterWrapperAdapter(_streamWriter), CultureInfo.InvariantCulture);
        }

        public void Close()
        {
            _csvWriter?.Dispose();
            _csvWriter = null;
            _streamWriter = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
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
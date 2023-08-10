using Common.Services;
using Common.Services.Interfaces;
using Common.Services.Mocks;
using NSubstitute;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTests
    {
        private IWriter? _csvWriter;

        private string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Path to a test CSV file

        private IStreamWriterWrapper? _streamWriterWrapper;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamWriterWrapperFactory _streamWriterWrapperFactory;


        public CsvWriterServiceTests()
        {
            _fileSystem = new MockFileSystem();
            var expectedLine = "Sample Line";
            var _streamWriterWrapperMock = Substitute.For<IStreamWriterWrapper>();
            _streamWriterWrapperMock.WriteLine(expectedLine);

            var streamWriterWrapperFactoryMock = Substitute.For<IStreamWriterWrapperFactory>();
            streamWriterWrapperFactoryMock.Create(_testFilePath).Returns(_streamWriterWrapperMock);

            _streamWriterWrapperFactory = streamWriterWrapperFactoryMock;
        }

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriter = new CsvWriterService(_fileSystem, _streamWriterWrapperFactory, _testFilePath);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _csvWriter.Dispose();

            DeleteMockedFile();
        }

        [TestMethod]
        public void Open_ThrowsObjectDisposedException_IfDisposed()
        {
            //TODO work on test
        }

        [TestMethod]
        public void WriteRecords_WhenFileDoesNotExist()
        {
            List<int> ints = new List<int>();

            Assert.ThrowsException<NotImplementedException>(() =>
            {
                // Call the synchronous method you want to test here
                _csvWriter.WriteRecords<int>(ints);  // Replace with the actual method call
            });
        }

        private void DeleteMockedFile()
        {
            if (_fileSystem.FileExists(_testFilePath))
            {
                _fileSystem.Delete(_testFilePath);
            }
        }
    }
}

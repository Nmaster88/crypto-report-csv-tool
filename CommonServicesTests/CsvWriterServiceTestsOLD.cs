using Common.Services;
using Common.Services.Interfaces;
using Common.Services.Mocks;
using NSubstitute;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTestsOLD
    {
        private IWriterOLD? _csvWriter;

        private string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Path to a test CSV file

        private IStreamWriterWrapper? _streamWriterWrapper;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamWriterWrapperFactory _streamWriterWrapperFactory;


        public CsvWriterServiceTestsOLD()
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
            this._csvWriter = new CsvWriterServiceOLD(_fileSystem, _streamWriterWrapperFactory);

            // Ensure the test file does not exist before each test
            if (_fileSystem.FileExists(_testFilePath))
            {
                _fileSystem.Delete(_testFilePath);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _csvWriter.Dispose();
            if (_fileSystem.FileExists(_testFilePath))
            {
                _fileSystem.Delete(_testFilePath);
            }
        }

        [TestMethod]
        public void Open_CreatesFile_WhenFileDoesNotExist()
        {
            // Arrange
            // Act
            _csvWriter.Open(_testFilePath);

            // Assert
            Assert.IsTrue(_fileSystem.FileExists(_testFilePath), "File should have been created.");
        }

        [TestMethod]
        public void Open_ThrowsObjectDisposedException_IfDisposed()
        {
            //TODO work on test
        }

        [TestMethod]
        public void WriteRecords_WhenFileDoesNotExist()
        { }

    }
}

using Common.Services;
using Common.Services.Interfaces;
using Common.Services.Mocks;
using Moq;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTests
    {
        private IWriter? _csvWriter;

        private string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Path to a test CSV file

        private IStreamReaderWrapper? _streamReaderWrapper;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamReaderWrapperFactory _streamReaderWrapperFactory;


        public CsvWriterServiceTests()     
        { 
            _fileSystem = new MockFileSystem();
            var expectedLine = "Sample Line";
            var _streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            _streamReaderWrapperMock.Setup(x => x.ReadLine()).Returns(expectedLine);

            //_streamReaderWrapperMock.Setup(x => x.)
            var streamReaderWrapperFactoryMock = new Mock<IStreamReaderWrapperFactory>();
            streamReaderWrapperFactoryMock.Setup(x => x.Create(_testFilePath)).Returns(_streamReaderWrapperMock.Object);
            _streamReaderWrapperFactory = streamReaderWrapperFactoryMock.Object;
            //_streamReader = new Mock<StreamReaderWrapper>().Object;
        }

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriter = new CsvWriterService(_fileSystem, _streamReaderWrapperFactory);

            // Ensure the test file does not exist before each test
            if(_fileSystem.FileExists(_testFilePath))
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

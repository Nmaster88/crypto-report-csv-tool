using Common.Services;
using Common.Services.Interfaces;
using Common.Services.Mocks;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTests
    {
        private IWriter? _csvWriter;

        private string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Path to a test CSV file

        private readonly IFileSystem _fileSystem;

        public CsvWriterServiceTests()     
        { 
            _fileSystem = new MockFileSystem();
        }


        private class TestRecord
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
        };

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriter = new CsvWriterService(_fileSystem);

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
        public void WriteRecords_WhenFileDoesNotExist()
        { }

    }
}

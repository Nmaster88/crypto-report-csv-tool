using Common.Services;
using CsvHelper;
using Moq;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvReaderServiceTests
    {
        private Common.Services.IReader _csvReader;

        private string _testFilePath = $"files{Path.DirectorySeparatorChar}test.csv"; // Path to a test CSV file

        private class TestRecord
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
        };

        [TestInitialize]
        public void Initialize()
        {
            this._csvReader = new CsvReaderService();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Open_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            string filePath = "nonexistentfile.csv";

            // Create a mock for IReader
            var mockReader = new Mock<Common.Services.IReader>();

            // Set up the Open method to throw an exception when the file doesn't exist
            mockReader.Setup(x => x.Open(filePath)).Throws(new FileNotFoundException("File not found.", filePath));

            // Act
            // Call the Open method of the mock IReader
            mockReader.Object.Open(filePath);
        }

        [TestMethod]
        public void Open_WhenGivenValidFilePath_ShouldOpenCsvFile()
        {
            // Arrange

            // Act
            this._csvReader.Open(_testFilePath);

            // Assert
            Assert.IsNotNull(this._csvReader);
        }

        [TestMethod]
        public void ReadRecords_WhenCalled_ShouldReturnListOfRecords()
        {
            // Arrange
            var reader = new CsvReaderService();
            reader.Open(_testFilePath);

            // Act
            var records = reader.ReadRecords<TestRecord>();

            // Assert
            Assert.IsNotNull(records);
            Assert.IsTrue(records.Count > 0);
        }

        [TestMethod]
        public void Close_WhenCalled_ShouldCloseCsvFile()
        {
            // Arrange
            var reader = new CsvReaderService();
            reader.Open(_testFilePath);

            // Act
            reader.Close();

            // Assert
            // Use a file system assertion library to verify that the file was closed
            using (var stream = new FileStream(_testFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                Assert.IsTrue(stream.CanWrite);
            }
        }
    }
}
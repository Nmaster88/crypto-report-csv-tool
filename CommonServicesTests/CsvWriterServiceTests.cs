using Common.Services;
using CsvHelper;
using Moq;
using System.IO;
using System.Reflection;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTests
    {
        private Common.Services.IWriter _csvWriter;

        private string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Path to a test CSV file


        private class TestRecord
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
        };

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriter = new CsvWriterService();

            // Ensure the test file does not exist before each test
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up test files created during the test
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [TestMethod]
        public void Open_CreatesFile_WhenFileDoesNotExist()
        {
            // Arrange
            // Act
            _csvWriter.Open(_testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath), "File should have been created.");
        }

        [TestMethod]
        public void Open_FileDoesNotExist_AfterClose()
        {
            // Arrange
            // Act
            _csvWriter.Open(_testFilePath);
            _csvWriter.Close();

            // Assert
            Assert.IsFalse(File.Exists(_testFilePath), "File should not exist after close.");
        }

    }
}

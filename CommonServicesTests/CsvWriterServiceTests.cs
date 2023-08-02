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
        [TestMethod]
        public void Open_CreatesFile_WhenFileDoesNotExist()
        {
            // Arrange
            string filePath = "testfile.csv";

            // Create a mock for CsvReader
            var mockCsvReader = new Mock<CsvReader>();
            mockCsvReader.Setup(x => x.Dispose());

            // Create a mock for StreamReader
            var mockStreamReader = new Mock<StreamReader>();
            mockStreamReader.Setup(x => x.Dispose());

            // Create the CsvWriteService instance
            var csvWriteService = new CsvWriteService();

            // Use reflection to access and set private fields
            var streamReaderField = csvWriteService.GetType().GetField("_streamReader", BindingFlags.Instance | BindingFlags.NonPublic);
            var csvReaderField = csvWriteService.GetType().GetField("_csvReader", BindingFlags.Instance | BindingFlags.NonPublic);

            streamReaderField.SetValue(csvWriteService, mockStreamReader.Object);
            csvReaderField.SetValue(csvWriteService, mockCsvReader.Object);

            // Act
            csvWriteService.Open(filePath);

            // Assert
            // Verify that Dispose method is called on CsvReader mock
            mockCsvReader.Verify(x => x.Dispose(), Times.Once);
            Assert.IsTrue(File.Exists(filePath), "File should have been created.");
        }
    }
}

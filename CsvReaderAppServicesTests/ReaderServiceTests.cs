using Common.Services.Interfaces;
using CsvReaderApp.Services;
using Moq;

namespace CsvReaderAppServicesTests
{
    [TestClass]
    public class ReaderServiceTests
    {
        private Mock<IReader> _mockReader;
        private ReaderService _readerService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockReader = new Mock<IReader>();
            _readerService = new ReaderService(_mockReader.Object);
        }

        [TestMethod]
        public void ReadRecords_ShouldCallReaderOpenMethod_WhenCalled()
        {
            // Arrange
            string filePath = "test-file.csv";

            // Act
            _readerService.ReadRecords<string>(filePath);

            // Assert
            _mockReader.Verify(x => x.Open(filePath), Times.Once);
        }

        [TestMethod]
        public void ReadRecords_ShouldCallReaderReadRecordsMethod_WhenCalled()
        {
            // Arrange
            string filePath = "test-file.csv";

            // Act
            _readerService.ReadRecords<string>(filePath);

            // Assert
            _mockReader.Verify(x => x.ReadRecords<string>(), Times.Once);
        }

        [TestMethod]
        public void ReadRecords_ShouldReturnRecords_WhenReaderReturnsRecords()
        {
            // Arrange
            string filePath = "test-file.csv";
            List<string> expectedRecords = new List<string> { "record1", "record2", "record3" };
            _mockReader.Setup(x => x.ReadRecords<string>()).Returns(expectedRecords);

            // Act
            List<string> actualRecords = _readerService.ReadRecords<string>(filePath);

            // Assert
            CollectionAssert.AreEqual(expectedRecords, actualRecords);
        }

        [TestMethod]
        public void ReadRecords_ShouldThrowArgumentNullException_WhenFilePathIsNull()
        {
            // Arrange
            string filePath = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _readerService.ReadRecords<string>(filePath));
        }
    }

}
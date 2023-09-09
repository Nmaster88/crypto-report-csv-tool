using Common.Services;
using Common.Services.Interfaces;
using Moq;
using NSubstitute;
using System.Text;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvReaderServiceTests
    {
        private IReader _csvReaderService;
        private string _testFilePath = $"files{Path.DirectorySeparatorChar}test.csv"; // Path to a test CSV file
        private readonly IStreamReaderWrapper? _streamReaderWrapper;
        private readonly IStreamReaderWrapperFactory? _streamReaderWrapperFactory;
        private class TestRecord
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
        };

        public CsvReaderServiceTests()
        {
            _streamReaderWrapper = Substitute.For<IStreamReaderWrapper>();

            _streamReaderWrapper.When(x => x.ReadLine())
                               .Do(callInfo =>
                               {
                                   string text = callInfo.Arg<string>();
                                   Console.WriteLine($"Writing text: {text}");
                               });

            _streamReaderWrapperFactory = Substitute.For<IStreamReaderWrapperFactory>();
            _streamReaderWrapperFactory.Create(_testFilePath).Returns(_streamReaderWrapper);
        }

        [TestInitialize]
        public void Initialize()
        {
            this._csvReaderService = new CsvReaderServiceOLD();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _csvReaderService?.Dispose();
        }

        [TestMethod]
        public void ReadRecords_WhenCalled_ShouldReturnListOfRecords()
        {
            // Arrange
            var reader = new CsvReaderService(_streamReaderWrapper);

            // Act
            var records = reader.ReadRecords<TestRecord>();

            // Assert
            Assert.IsNotNull(records);
            Assert.IsTrue(records.Count > 0);
        }
    }
}
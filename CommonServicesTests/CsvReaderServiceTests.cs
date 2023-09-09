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
            //_streamReaderWrapper.GetEncoding().Returns(Encoding.UTF8);

            _streamReaderWrapper.When(x => x.ReadLine())
                               .Do(callInfo =>
                               {
                                   string text = callInfo.Arg<string>();
                                   Console.WriteLine($"Writing text: {text}");
                               });

            //_streamReaderWrapper.When(x => x.Flush())
            //                   .Do(_ =>
            //                   {
            //                       Console.WriteLine("Flush called");
            //                   });

            //_streamReaderWrapper.When(x => x.Write(Arg.Any<char[]>(), Arg.Any<int>(), Arg.Any<int>()))
            //       .Do(callInfo =>
            //       {
            //           char[] buffer = callInfo.ArgAt<char[]>(0);
            //           string text = new string(buffer);
            //           Console.WriteLine($"Writing text: {text}");
            //       });

            //_streamReaderWrapper.WriteAsync(Arg.Any<char[]>(), Arg.Any<int>(), Arg.Any<int>())
            //                   .Returns(Task.CompletedTask);

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

        //[TestMethod]
        //[ExpectedException(typeof(FileNotFoundException))]
        //public void Open_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        //{
        //    // Arrange
        //    string filePath = "nonexistentfile.csv";

        //    // Create a mock for IReader
        //    var mockReader = new Mock<IReader>();

        //    // Set up the Open method to throw an exception when the file doesn't exist
        //    mockReader.Setup(x => x.Open(filePath)).Throws(new FileNotFoundException("File not found.", filePath));

        //    // Act
        //    // Call the Open method of the mock IReader
        //    mockReader.Object.Open(filePath);
        //}

        //[TestMethod]
        //public void Open_SameFileTwice_Success()
        //{
        //    // Arrange
        //    var readerService = new CsvReaderService();

        //    // Act
        //    readerService.Open(_testFilePath);
        //    var records = readerService.ReadRecords<TestRecord>();
        //    //readerService.Close();

        //    // Open the same file again
        //    readerService.Open(_testFilePath);
        //    var records2 = readerService.ReadRecords<TestRecord>();
        //    readerService.Close();

        //    // Assert
        //    Assert.IsNotNull(records);
        //    Assert.IsTrue(records.Count > 0);

        //    Assert.IsNotNull(records2);
        //    Assert.IsTrue(records2.Count > 0);
        //}

        //[TestMethod]
        //public void Open_WhenGivenValidFilePath_ShouldOpenCsvFile()
        //{
        //    // Arrange

        //    // Act
        //    this._csvReaderService.Open(_testFilePath);

        //    // Assert
        //    Assert.IsNotNull(this._csvReaderService);
        //}

        [TestMethod]
        public void ReadRecords_WhenCalled_ShouldReturnListOfRecords()
        {
            // Arrange
            var reader = new CsvReaderServiceOLD();
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
            var reader = new CsvReaderServiceOLD();
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
using Common.Services;
using Common.Services.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using NSubstitute;
using System.Globalization;
using System.IO;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTests
    {
        private Common.Services.Interfaces.IWriter? _csvWriterService;
        private readonly string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Path to a test CSV file
        private readonly string _expectedLine = "Sample Line";
        private readonly IStreamWriterWrapper? _streamWriterWrapper;
        private readonly IFileSystem _fileSystem;
        private readonly IStreamWriterWrapperFactory _streamWriterWrapperFactory;
        private class CsvWriterRecord 
        {
            public string Col1 { get; set; }
        }

        public CsvWriterServiceTests()
        {
            _fileSystem = Substitute.For<IFileSystem>();

            _streamWriterWrapper = Substitute.For<IStreamWriterWrapper>();

            //_streamWriterWrapper
            //    .WhenForAnyArgs(x => x.WriteLine(_expectedLine))
            //    .Do(callInfo =>
            //    {
            //        // Call the action that was passed to WriteLine
            //        var action = callInfo.Arg<Action<string>>();
            //        action(_expectedLine);

            //        // Call the equivalent WriteAllText method with the captured line and file path
            //        _fileSystem.WriteAllText(_testFilePath, _expectedLine);
            //    });

            var streamWriterWrapperFactoryMock = Substitute.For<IStreamWriterWrapperFactory>();
            streamWriterWrapperFactoryMock.Create(_testFilePath).Returns(_streamWriterWrapper);

            _streamWriterWrapperFactory = streamWriterWrapperFactoryMock;
        }

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriterService = new CsvWriterService(_fileSystem, _streamWriterWrapperFactory, _testFilePath);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _csvWriterService.Dispose();

            DeleteMockedFile();
        }

        [TestMethod]
        public void WriteRecords_WhenFileDoesNotExist()
        {

            List<CsvWriterRecord> lines = new List<CsvWriterRecord>();
            lines.Add(new CsvWriterRecord() { Col1 = "Sample Line" });

            //Assert
            _csvWriterService.WriteRecords(lines);
        }

        private void DeleteMockedFile()
        {
            if (_fileSystem.FileExists(_testFilePath))
            {
                _fileSystem.DeleteFile(_testFilePath);
            }
        }
    }
}

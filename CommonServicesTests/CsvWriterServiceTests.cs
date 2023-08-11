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
        private readonly string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Maybe we dont want to create a real file
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

            var streamWriterWrapperFactoryMock = Substitute.For<IStreamWriterWrapperFactory>();
            streamWriterWrapperFactoryMock.Create(_testFilePath).Returns(_streamWriterWrapper);

            _streamWriterWrapperFactory = streamWriterWrapperFactoryMock;
        }

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriterService = new CsvWriterService(_streamWriterWrapperFactory, _testFilePath);
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

            //Act
            _csvWriterService.WriteRecords(lines);
            //mock for _streamWriterWrapper?

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

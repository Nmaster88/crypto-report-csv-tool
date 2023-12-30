using Common.Services;
using Common.Services.Interfaces;
using NSubstitute;
using System.Text;

namespace CommonServicesTests
{
    [TestClass]
    public class CsvWriterServiceTests
    {
        private Common.Services.Interfaces.IWriter? _csvWriterService;
        private readonly string _testFilePath = $"files{Path.DirectorySeparatorChar}testwrite.csv"; // Maybe we dont want to create a real file
        private readonly IStreamWriterWrapper? _streamWriterWrapper;
        private readonly IStreamWriterWrapperFactory? _streamWriterWrapperFactory;
        private class CsvWriterRecord
        {
            public string Col1 { get; set; }
        }

        public CsvWriterServiceTests()
        {
            _streamWriterWrapper = Substitute.For<IStreamWriterWrapper>();
            _streamWriterWrapper.GetEncoding().Returns(Encoding.UTF8);

            // Example behavior for WriteLine
            _streamWriterWrapper.When(x => x.WriteLine(Arg.Any<string>()))
                               .Do(callInfo =>
                               {
                                   string text = callInfo.Arg<string>();
                                   Console.WriteLine($"Writing text: {text}");
                               });

            // Example behavior for Flush
            _streamWriterWrapper.When(x => x.Flush())
                               .Do(_ =>
                               {
                                   Console.WriteLine("Flush called");
                               });

            _streamWriterWrapper.When(x => x.Write(Arg.Any<char[]>(), Arg.Any<int>(), Arg.Any<int>()))
                   .Do(callInfo =>
                   {
                       char[] buffer = callInfo.ArgAt<char[]>(0);
                       string text = new string(buffer);
                       Console.WriteLine($"Writing text: {text}");
                   });

            // Example behavior for WriteAsync
            _streamWriterWrapper.WriteAsync(Arg.Any<char[]>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(Task.CompletedTask);

            _streamWriterWrapperFactory = Substitute.For<IStreamWriterWrapperFactory>();
            _streamWriterWrapperFactory.Create(_testFilePath).Returns(_streamWriterWrapper);
        }

        [TestInitialize]
        public void Initialize()
        {
            this._csvWriterService = new CsvWriterService(_streamWriterWrapper);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _csvWriterService?.Dispose();
        }

        [TestMethod]
        public void WriteRecords_WhenFileDoesNotExist()
        {

            List<CsvWriterRecord> lines = new List<CsvWriterRecord>();
            lines.Add(new CsvWriterRecord() { Col1 = "Sample Line" });

            //Act
            _csvWriterService.WriteRecords(lines);

            //Assert
            _streamWriterWrapper.Received(2).Write(Arg.Any<char[]>(), Arg.Any<int>(), Arg.Any<int>());
        }
    }
}

// See https://aka.ms/new-console-template for more information
using AutoMapper;
using Common.Services;
using Common.Services.Interfaces;
using CsvReaderApp.Binance.Models;
using CsvReaderApp.Console.Utils;
using CsvReaderApp.Models;
using CsvReaderApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = ConfigurationSetup();

var serviceProvider = ConfigureServices();
var mapper = serviceProvider.GetRequiredService<IMapper>();

string directory = configuration.GetSection("AppSettings").GetValue<string>("Directory") ?? "C:\\Users\\Nuno\\Downloads";
string fileName = configuration.GetSection("AppSettings").GetValue<string>("FileName") ?? "binance_report_2021.csv";

List<AccountReportResult> destinationList = new List<AccountReportResult>();

bool moreFiles = false;
do
{
    Console.WriteLine("What is the Path for the file? Leave empty to use app settings value.");
    string inputFileName = Console.ReadLine();
    string filePath = !string.IsNullOrEmpty(inputFileName) ? inputFileName : Path.GetFullPath($"{directory}{Path.DirectorySeparatorChar}{fileName}");

    IReader reader = new CsvReaderServiceOLD();
    ReaderService readerService = new ReaderService(reader);
    var binanceReport = readerService.ReadRecords<ReportEntry>(filePath);

    destinationList.AddRange(mapper.Map<List<AccountReportResult>>(binanceReport));

    Console.WriteLine("Do you want to add another file? Y/N");
    moreFiles = Console.ReadLine().ToLower() == "y" ? true : false;
}
while (moreFiles);

ConsoleCommunication consoleCommunication = new ConsoleCommunication();

BinanceReportService accountReportService = new BinanceReportService(consoleCommunication);

consoleCommunication.SendMessage("");
consoleCommunication.SendMessage("---ReportSummary---");
consoleCommunication.SendMessage("");

accountReportService.ReportSummary(destinationList);

consoleCommunication.SendMessage("");
consoleCommunication.SendMessage("---ReportTransactionsByCoin  EUR---");
consoleCommunication.SendMessage("");

accountReportService.ReportTransactionsWithCoin(destinationList, "EUR");

consoleCommunication.SendMessage("");
consoleCommunication.SendMessage("---ReportDistincOperations---");
consoleCommunication.SendMessage("");

accountReportService.ReportDistinctOperations(destinationList);

consoleCommunication.SendMessage("");
consoleCommunication.SendMessage("---ReportByCoin---");
consoleCommunication.SendMessage("");

accountReportService.ReportByCoin(destinationList, "EUR");

consoleCommunication.SendMessage("");
consoleCommunication.SendMessage("---ReportTransactionsOutAndRelatedWithCoin---");
consoleCommunication.SendMessage("");

accountReportService.ReportTransactionsOutAndRelatedWithCoin(destinationList, "EUR");

Console.ReadLine();


//TODO: study the possiblity to replace ObjectAssignementService and ConvertService with AutoMapper
//ObjectAssignementService<List<BinanceReportEntry>, List<AccountReportResult>> objectAssignmentService = new ObjectAssignementService<List<BinanceReportEntry>, List<AccountReportResult>>();
//objectAssignmentService.Setup();
//objectAssignmentService.Mapping();

//ConvertService<List<BinanceReportEntry>, List<AccountReportResult>> convertService = new ConvertService<List<BinanceReportEntry>, List<AccountReportResult>>(objectAssignmentService);
//convertService.Convert(binanceReport, new List<AccountReportResult>());


IConfiguration ConfigurationSetup()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.json", true, true);
    return builder.Build();
}

IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    var mapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<AutoMapperProfile>();
    });

    IMapper mapper = mapperConfig.CreateMapper();
    services.AddSingleton(mapper)
            .AddSingleton<ICommunication, ConsoleCommunication>();

    var serviceProvider = services.BuildServiceProvider();
    return serviceProvider;
}
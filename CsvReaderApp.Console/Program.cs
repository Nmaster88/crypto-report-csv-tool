// See https://aka.ms/new-console-template for more information
using AutoMapper;
using Common.Services;
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
Console.WriteLine("What is the Path for the file? Leave empty to use app settings value.");
string inputFileName = Console.ReadLine();
string filePath = !string.IsNullOrEmpty(inputFileName) ? inputFileName : Path.GetFullPath($"{directory}{Path.DirectorySeparatorChar}{fileName}");


IReader reader = new CsvReaderService();
ReaderService readerService = new ReaderService(reader);
var binanceReport = readerService.ReadRecords<BinanceReportEntry>(filePath);

var destinationList = mapper.Map<List<AccountReportResult>>(binanceReport);

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
    services.AddSingleton(mapper);

    var serviceProvider = services.BuildServiceProvider();
    return serviceProvider;
}
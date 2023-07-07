// See https://aka.ms/new-console-template for more information
using Common.Services;
using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using CsvReaderApp.Services;
using Microsoft.Extensions.Configuration;

var configuration = ConfigurationSetup();

string directory = configuration.GetSection("AppSettings").GetValue<string>("Directory") ?? "C:\\Users\\Nuno\\Downloads";
string fileName = configuration.GetSection("AppSettings").GetValue<string>("FileName") ?? "binance_report_2021.csv";

Console.WriteLine("What is the Path for the file? Leave empty to use app settings value.");
string inputFileName = Console.ReadLine();
string filePath = !string.IsNullOrEmpty(inputFileName) ? inputFileName : Path.GetFullPath($"{directory}{Path.DirectorySeparatorChar}{fileName}");


IReader reader = new CsvReaderService();
ReaderService readerService = new ReaderService(reader);
var binanceReport = readerService.ReadRecords<BinanceReportEntry>(filePath);

//BinanceReportService binanceReportService = new BinanceReportService();
//binanceReportService.Execute(binanceReport);
ConvertService binanceReportService = new ConvertService();
//binanceReportService.Execute(binanceReport);
List<AccountReportResult> resultsDestiny = new List<AccountReportResult>();
binanceReportService.ExecuteAssignment(binanceReport, resultsDestiny);

static IConfiguration ConfigurationSetup()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.json", true, true);
    //.AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true, reloadOnChange: true)
    return builder.Build();
}
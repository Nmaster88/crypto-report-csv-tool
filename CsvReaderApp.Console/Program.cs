// See https://aka.ms/new-console-template for more information
using CsvReaderApp.Models;
using CsvReaderApp.Services;
using Microsoft.Extensions.Configuration;

var configuration = ConfigurationSetup();

string directory = configuration.GetSection("AppSettings").GetValue<string>("Directory") ?? "C:\\Users\\nmoncheira\\Downloads";
string fileName = configuration.GetSection("AppSettings").GetValue<string>("FileName") ?? "binance_report_2021.csv";

string filePath = Path.GetFullPath($"{directory}{Path.DirectorySeparatorChar}{fileName}");


ReaderService readerService = new ReaderService(filePath);
var binanceReport = readerService.ReadRecords<BinanceReport>();

BinanceReportService binanceReportService = new BinanceReportService();
binanceReportService.Execute(binanceReport);

static IConfiguration ConfigurationSetup()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.json", true, true);
    //.AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true, reloadOnChange: true)
    return builder.Build();
}
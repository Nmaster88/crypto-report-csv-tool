// See https://aka.ms/new-console-template for more information
using CsvReaderApp.Models;
using CsvReaderApp.Services;

Console.WriteLine("Hello, World!");

string directoryPath = "C:\\Users\\Nuno\\Downloads";
string fileName = "binance_report_2021.csv";
string filePath = Path.GetFullPath($"{directoryPath}{Path.DirectorySeparatorChar}{fileName}");


ReaderService readerService = new ReaderService(filePath);
var binanceReport = readerService.ReadRecords<BinanceReport>();

BinanceReportService binanceReportService = new BinanceReportService();
binanceReportService.Execute(binanceReport);
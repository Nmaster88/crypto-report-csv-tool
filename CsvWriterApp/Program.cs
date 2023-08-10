using Common.Services;
using Common.Services.Interfaces;

IFileSystem fileSystem = new FileSystem();
IStreamWriterWrapperFactory systemWriterWrapperFactory = new StreamWriterWrapperFactory();

CsvWriterService csvWriterService = new CsvWriterService(fileSystem, systemWriterWrapperFactory, $"C:{Path.DirectorySeparatorChar}Users{Path.DirectorySeparatorChar}nunog{Path.DirectorySeparatorChar}Downloads{Path.DirectorySeparatorChar}filetodelete.csv");



List<CsvWriterRecord> lines = new List<CsvWriterRecord>();
lines.Add(new CsvWriterRecord() { Col1 = "Sample Line" });

csvWriterService.WriteRecords(lines);

csvWriterService.Dispose();

public class CsvWriterRecord
{
    public string Col1 { get; set; }
}
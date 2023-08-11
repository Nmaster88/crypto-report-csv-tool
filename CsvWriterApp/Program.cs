using Common.Services;
using Common.Services.Interfaces;

IStreamWriterWrapperFactory systemWriterWrapperFactory = new StreamWriterWrapperFactory();
string filePath = $"C:{Path.DirectorySeparatorChar}Users{Path.DirectorySeparatorChar}nunog{Path.DirectorySeparatorChar}Downloads{Path.DirectorySeparatorChar}filetodelete.csv";
//CsvWriterService csvWriterService = new CsvWriterService(systemWriterWrapperFactory, $"C:{Path.DirectorySeparatorChar}Users{Path.DirectorySeparatorChar}nunog{Path.DirectorySeparatorChar}Downloads{Path.DirectorySeparatorChar}filetodelete.csv");



//List<CsvWriterRecord> lines = new List<CsvWriterRecord>();
//lines.Add(new CsvWriterRecord() { Col1 = "Sample Line" });

using (CsvWriterService csvWriterService = new CsvWriterService(systemWriterWrapperFactory, filePath))
{
    List<CsvWriterRecord> lines = new List<CsvWriterRecord>();
    lines.Add(new CsvWriterRecord() { Col1 = "Sample Line" });

    //List<string> lines = new List<string>();
    //lines.Add("Sample Line");

    csvWriterService.WriteRecords(lines);

    // No need to call csvWriterService.Dispose() here, using block will take care of it.
}

Console.WriteLine("CSV file written successfully.");

public class CsvWriterRecord
{
    public string Col1 { get; set; }
}
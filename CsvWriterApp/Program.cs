using Common.Services;

IStreamWriterWrapperFactory systemWriterWrapperFactory = new StreamWriterWrapperFactory();
string filePath = $"C:{Path.DirectorySeparatorChar}Users{Path.DirectorySeparatorChar}nunog{Path.DirectorySeparatorChar}Downloads{Path.DirectorySeparatorChar}filetodelete.csv";

using (CsvWriterService csvWriterService = new CsvWriterService(systemWriterWrapperFactory, filePath))
{
    List<CsvWriterRecord> lines = new List<CsvWriterRecord>();
    lines.Add(new CsvWriterRecord() { Col1 = "Sample Line" });

    csvWriterService.WriteRecords(lines);
}

Console.WriteLine("CSV file written successfully.");

public class CsvWriterRecord
{
    public string Col1 { get; set; }
}
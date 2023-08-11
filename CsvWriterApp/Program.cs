using Common.Services;
using Common.Services.Interfaces;

string filePath = $"C:{Path.DirectorySeparatorChar}Users{Path.DirectorySeparatorChar}nunog{Path.DirectorySeparatorChar}Downloads{Path.DirectorySeparatorChar}filetodelete.csv";

using (IWriter csvWriterService = new CsvWriterFactory(new StreamWriterWrapperFactory()).Create(filePath))
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
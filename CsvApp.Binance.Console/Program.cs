using Common.Services;
using Common.Services.Interfaces;
using CsvApp.Binance.Application.Interfaces;
using CsvApp.Binance.Application.Services;
using CsvApp.Binance.Domain.Interfaces;
using CsvApp.Binance.Infrastructure;
using CsvReaderApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = ConfigurationSetup();

string directory = configuration.GetSection("AppSettings").GetValue<string>("Directory") ?? "C:\\Users\\Nuno\\Downloads";
string fileNameToRead = configuration.GetSection("AppSettings").GetValue<string>("FileNameToRead") ?? "binance_report_2021.csv";
string fileNameToWrite = "output_text.txt";

var serviceProvider = ConfigureServices(Path.GetFullPath($"{directory}{Path.DirectorySeparatorChar}{fileNameToRead}"));

ICsvApp csvApp = serviceProvider.GetRequiredService<ICsvApp>();

var result = await csvApp.GetRealizedCapitalGains();

try
{
    using (StreamWriter writer = new StreamWriter($"{directory}{Path.DirectorySeparatorChar}{fileNameToWrite}"))
    {
        foreach (var item in result)
        {
            writer.WriteLine(item);
        }
    }

    Console.WriteLine("Values written to the file successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

IConfiguration ConfigurationSetup()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.json", true, true);
    return builder.Build();
}

IServiceProvider ConfigureServices(string filePath)
{
    var services = new ServiceCollection();

    services
            .AddSingleton<ICommunication, ConsoleCommunication>()
            .AddSingleton<IReader, CsvReaderService>()
            .AddSingleton<FilesRepositoryBuilder>()
            .AddSingleton<ICsvApp, CsvAppEntryPoint>()
            .AddSingleton<ICsvAppDownload, CsvAppDownload>()
            .AddSingleton<ICsvAppProcess, CsvAppProcess>()
            .AddSingleton<IFilesRepository>(provider =>
            {
                var factory = provider.GetRequiredService<FilesRepositoryBuilder>();
                return factory.Build(filePath);
            });
    ;

    var serviceProvider = services.BuildServiceProvider();
    return serviceProvider;
}
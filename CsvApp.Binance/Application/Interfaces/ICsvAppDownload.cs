using CsvApp.Binance.Application.Dtos;

namespace CsvApp.Binance.Application.Interfaces
{
    public interface ICsvAppDownload
    {
        Task<List<IncomeGainsDto>> DownloadIncomeGains();
        Task<List<TransactionsDto>> DownloadTransations();
        Task<List<RealizedCapitalGainsDto>> DownloadRealizedCapitalGains();
    }
}

using CsvApp.Binance.Domain.Entities;

namespace CsvApp.Binance.Domain.Interfaces
{
    public interface IFilesRepository
    {
        Task<List<TransactionsEntity>?> GetTransactions();
        Task<List<IncomeGainsEntity>?> GetIncomeGains();
        Task<List<RealizedCapitalGainsEntity>?> GetRealizedCapitalGains();
    }
}

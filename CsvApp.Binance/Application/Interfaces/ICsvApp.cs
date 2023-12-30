using CsvApp.Binance.Application.Dtos;

namespace CsvApp.Binance.Application.Interfaces
{
    public interface ICsvApp
    {
        public Task<List<IncomeGainsDto>> GetIncomeGains();
        public Task<List<TransactionsDto>> GetTransactions();
        public Task<List<string>> GetRealizedCapitalGains();
    }
}

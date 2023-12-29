using CsvApp.Binance.Application.Dtos;

namespace CsvApp.Binance.Application.Interfaces
{
    internal interface ICsvApp
    {
        public Task<List<IncomeGainsDto>> GetIncomeGains();
        public Task<List<TransactionsDto>> GetTransactions();
    }
}

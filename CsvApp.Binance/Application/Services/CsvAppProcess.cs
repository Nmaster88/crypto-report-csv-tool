using CsvApp.Binance.Application.Dtos;
using CsvApp.Binance.Application.Interfaces;

namespace CsvApp.Binance.Application.Services
{
    internal class CsvAppProcess : ICsvAppProcess
    {
        public List<IncomeGainsDto> Process(List<IncomeGainsDto> list)
        {
            throw new NotImplementedException();
        }

        public List<TransactionsDto> Process(List<TransactionsDto> list)
        {
            throw new NotImplementedException();
        }
    }
}

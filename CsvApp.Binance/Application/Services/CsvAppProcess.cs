using CsvApp.Binance.Application.Dtos;
using CsvApp.Binance.Application.Interfaces;

namespace CsvApp.Binance.Application.Services
{
    internal class CsvAppProcess : ICsvAppProcess
    {
        public List<IncomeGainsDto> Process(List<IncomeGainsDto> list)
        {
            var result = list
                .GroupBy(i => i.Asset)
                .Select(v => new
                {
                    Asset = v.Key,
                    TotalValueEUR = v.Sum(t => t.Value)
                }
                );
            throw new NotImplementedException();
        }

        public List<TransactionsDto> Process(List<TransactionsDto> list)
        {
            throw new NotImplementedException();
        }
    }
}

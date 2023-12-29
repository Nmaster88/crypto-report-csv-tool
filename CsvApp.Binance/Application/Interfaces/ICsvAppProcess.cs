using CsvApp.Binance.Application.Dtos;

namespace CsvApp.Binance.Application.Interfaces
{
    public interface ICsvAppProcess
    {
        List<IncomeGainsDto> Process(List<IncomeGainsDto> list);
        List<TransactionsDto> Process(List<TransactionsDto> list);
    }
}

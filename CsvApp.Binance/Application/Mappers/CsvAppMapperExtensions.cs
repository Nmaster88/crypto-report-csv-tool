using CsvApp.Binance.Application.Dtos;
using CsvApp.Binance.Domain.Entities;

namespace CsvApp.Binance.Application.Mappers
{
    public static class CsvAppMapperExtensions
    {
        public static IEnumerable<IncomeGainsDto> ToViews(this IEnumerable<IncomeGainsEntity> entities)
        {
            return entities.Select(incomeGains =>
            {
                return new IncomeGainsDto(
                    incomeGains.Date,
                    incomeGains.Asset,
                    incomeGains.Amount,
                    incomeGains.PricePerUnit,
                    incomeGains.Value,
                    incomeGains.TransactionType,
                    incomeGains.Label);
            });
        }

        public static IEnumerable<TransactionsDto> ToViews(this IEnumerable<TransactionsEntity> entities)
        {
            return entities.Select(transactions =>
            {
                return new TransactionsDto(
                    transactions.Id,
                    transactions.Date,
                    transactions.Type,
                    transactions.Label,
                    transactions.SentAmount,
                    transactions.SentCurrency,
                    transactions.SentAddress,
                    transactions.ReceivedAmount,
                    transactions.ReceivedCurrency,
                    transactions.FeeAmount,
                    transactions.FeeCurrency,
                    transactions.Comment);
            });
        }
    }
}

namespace CsvApp.Binance.Application.Dtos
{
    public record IncomeGainsDto(
        DateTime Date,
        string Asset,
        decimal Amount,
        decimal PricePerUnit /*Price per unit (EUR)*/,
        decimal Value /*Value (EUR)*/,
        string TransactionType /*Transaction Type*/,
        string Label
        );
}

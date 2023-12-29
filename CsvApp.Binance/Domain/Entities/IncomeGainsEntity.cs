namespace CsvApp.Binance.Domain.Entities
{
    public record IncomeGainsEntity(
        DateTime Date,
        string Asset,
        decimal Amount,
        decimal PricePerUnit /*Price per unit (EUR)*/,
        decimal Value /*Value (EUR)*/,
        string TransactionType /*Transaction Type*/,
        string Label
        );
}

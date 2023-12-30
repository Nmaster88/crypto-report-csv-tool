namespace CsvApp.Binance.Domain.Entities;
public record RealizedCapitalGainsEntity(
        string CurrencyName,
        decimal CurrencyAmount,
        string Acquired,
        string Sold,
        decimal ProceedsEur,
        decimal CostBasisEur,
        decimal GainsEur,
        int HoldingPeriodDays,
        string TransactionType,
        string Label
        );
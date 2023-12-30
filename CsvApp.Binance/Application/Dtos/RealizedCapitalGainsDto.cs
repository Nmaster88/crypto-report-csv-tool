namespace CsvApp.Binance.Application.Dtos;
public record RealizedCapitalGainsDto(
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
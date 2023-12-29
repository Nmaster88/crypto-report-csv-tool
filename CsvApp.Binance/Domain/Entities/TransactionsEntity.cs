namespace CsvApp.Binance.Domain.Entities;
public record TransactionsEntity(
        string Id,
        DateTime Date,
        string Type,
        string Label,
        decimal SentAmount /*Sent Amount*/,
        string SentCurrency /*Sent Currency*/,
        string SentAddress /*Sent Address*/,
        decimal ReceivedAmount /*Received Amount*/,
        string ReceivedCurrency /*Received Currency*/,
        decimal FeeAmount /*Fee Amount*/,
        string FeeCurrency /*Fee Currency*/,
        string Comment
        );

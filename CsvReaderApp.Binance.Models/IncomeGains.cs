﻿namespace CsvReaderApp.Binance.Models
{
    public record IncomeGains(
        DateTime Date,
        string Asset,
        decimal Amount,
        decimal PricePerUnit /*Price per unit (EUR)*/,
        decimal Value /*Value (EUR)*/,
        string TransactionType /*Transaction Type*/,
        string Label
        );
}

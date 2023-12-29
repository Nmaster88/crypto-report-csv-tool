using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Binance.Models
{
    public record Transactions (
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
}

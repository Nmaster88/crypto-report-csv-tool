using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Binance.Models
{
    public record IncomeGains (
        DateTime Date, 
        string Asset, 
        decimal Amount, 
        decimal PricePerUnit /*Price per unit (EUR)*/,
        decimal Value /*Value (EUR)*/,
        string TransactionType /*Transaction Type*/,
        string Label
        );
}

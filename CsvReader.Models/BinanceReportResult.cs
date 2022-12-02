﻿namespace CsvReaderApp.Models
{
    public class BinanceReportResult
    {
        public string Operation { get; set; }
        public string Coin { get; set; }
        public decimal Change { get; set; }
        public string Remark { get; set; }
    }
}

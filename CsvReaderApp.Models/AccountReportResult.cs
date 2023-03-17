namespace CsvReaderApp.Models
{
    public class AccountReportResult
    {
        public string Operation { get; set; } = string.Empty;
        public string Coin { get; set; } = string.Empty;
        public decimal Change { get; set; }
        public string Remark { get; set; } = string.Empty;
    }
}

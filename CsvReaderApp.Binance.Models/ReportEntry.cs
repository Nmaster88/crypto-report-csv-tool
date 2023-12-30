namespace CsvReaderApp.Binance.Models
{
    [Obsolete("to be migrated")]
    public class ReportEntry
    {
        public int User_ID { get; set; }
        public DateTime UTC_Time { get; set; }
        public string Account { get; set; }
        public string Operation { get; set; }
        public string Coin { get; set; }
        public string Change { get; set; }
        public string Remark { get; set; }
    }
}
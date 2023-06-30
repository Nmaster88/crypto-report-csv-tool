namespace CsvReaderApp.Models
{
    public class BinanceReportEntry
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
namespace CsvReaderApp.Models
{
    public class BinanceReport
    {
        public int User_Id { get; set; }
        public DateTime UTC_Time { get; set; }
        public string Account { get; set; }
        public string Operation { get; set; }
        public string Coin { get; set; }
        public int Change { get; set; }
        public string Remark { get; set; }
    }
}
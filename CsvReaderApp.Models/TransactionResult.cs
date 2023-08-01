namespace CsvReaderApp.Models
{
    public class TransactionResult
    {
        public string InCoin { get; set; }
        public string OutCoin { get; set; }
        public int TransactionInId { get; set; }
        public int TransactionOutId { get; set; }
        public decimal QuantityIn { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal QuantityInMissing { get; set; }
        public bool TransactionInFilled { get; set; }
        public bool OneYearOrMore { get; set; }
        public DateTime BuyAndSellInterval { get; set; }

    }
}

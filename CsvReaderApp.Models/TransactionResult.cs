namespace CsvReaderApp.Models
{
    public class TransactionResult
    {
        public int TransactionInId { get; set; }
        public int TransactionOutId { get; set; }
        public decimal QuantityIn { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal QuantityInMissing { get; set; }
        public bool TransactionInFilled { get; set; }

    }
}

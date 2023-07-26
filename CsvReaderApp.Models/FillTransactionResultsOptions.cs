namespace CsvReaderApp.Models
{
    public class FillTransactionResultsOptions
    {
        public AccountReportResult TransactionOut { get; set; }
        public List<AccountReportResult> IncreaseAccountReportListByCoin { get; set; }
        public List<TransactionResult> TransactionList { get; set; }
    }

}

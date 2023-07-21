using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;

namespace CsvReaderApp.Services
{
    public interface IBinanceReportService
    {
        void ReportSummary(List<AccountReportResult> accountReportResultList);
        void ReportTransactions(List<AccountReportResult> accountReportResultList);
    }
    public class BinanceReportService : IBinanceReportService
    {
        private readonly ICommunication _communication;
        public BinanceReportService(ICommunication communication) 
        {
            _communication = communication;
        }

        public void ReportSummary(List<AccountReportResult> accountReportResultList)
        {
            if (accountReportResultList == null)
            {
                return;
            }

            var accountReportAccountList = accountReportResultList.Where(x => x.Account == AccountEnum.Spot.ToString()).ToList();

            var allOperationValues = Enum.GetValues(typeof(OperationEnum));
            foreach (OperationEnum operationValue in allOperationValues)
            {
                GroupByValue(accountReportAccountList, operationValue.ToString());
            }
        }

        public void ReportTransactions(List<AccountReportResult> accountReportResultList)
        {
            if (accountReportResultList == null)
            {
                return;
            }

            string transactionBuy = OperationEnum.Transaction_Buy.ToString().Replace("_", " ");
            string transactionSpend = OperationEnum.Transaction_Spend.ToString().Replace("_", " ");
            string transactionRevenue = OperationEnum.Transaction_Revenue.ToString().Replace("_", " ");
            string transactionSold = OperationEnum.Transaction_Sold.ToString().Replace("_", " ");
            string fee = OperationEnum.Fee.ToString();
            string referralCommission = OperationEnum.Referral_Commission.ToString().Replace("_", " ");

            var accountReportAccountList = accountReportResultList.Where(x => x.Account == AccountEnum.Spot.ToString() 
            && (x.Operation == transactionBuy || x.Operation == transactionSpend || x.Operation == transactionRevenue || x.Operation == transactionSold || x.Operation == fee || x.Operation == referralCommission))
                .GroupBy(x => x.DateTime).ToList();
        }

        private void GroupByValue(List<AccountReportResult> accountReportResultList, string value)
        {
            var groupList = accountReportResultList.Where(x => x.Operation.Contains(value.Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });
            if (groupList != null && groupList.Count() > 0)
            {
                _communication.SendMessage($"Total {value} By coin:");
                foreach (var accountReport in groupList)
                {
                    _communication.SendMessage($"| {accountReport.Coin} | {accountReport.Change} |");
                }
            }
        }
    }
}

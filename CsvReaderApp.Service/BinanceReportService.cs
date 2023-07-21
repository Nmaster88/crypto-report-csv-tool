using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;

namespace CsvReaderApp.Services
{
    public interface IBinanceReportService
    {
        void ReportSummary(List<AccountReportResult> accountReportResultList);
        void ReportTransactions(List<AccountReportResult> accountReportResultList);
        void ReportTransactionsWithCoin(List<AccountReportResult> accountReportResultList, string coin);
        void ReportDistinctOperations(List<AccountReportResult> accountReportResultList);
        void ReportByCoin(List<AccountReportResult> accountReportResultList, string coin);
    }
    public class BinanceReportService : IBinanceReportService
    {
        private readonly ICommunication _communication;
        public BinanceReportService(ICommunication communication) 
        {
            _communication = communication;
        }

        public void ReportByCoin(List<AccountReportResult> accountReportResultList, string coin)
        {
            if (accountReportResultList == null)
            {
                return;
            }

            var accountReportByCoinList = accountReportResultList.Where(x => x.Coin == coin).ToList();

            decimal positive = accountReportByCoinList.Where(x => x.Change > 0.0m).Sum(x => x.Change);
            decimal negative = accountReportByCoinList.Where(x => x.Change < 0.0m).Sum(x => x.Change);
            decimal difference = positive + negative;

            _communication.SendMessage($"Coin: {coin}");
            _communication.SendMessage($"positive: {positive}");
            _communication.SendMessage($"negative: {negative}");
            _communication.SendMessage($"difference: {difference}");
        }

        public void ReportDistinctOperations(List<AccountReportResult> accountReportResultList)
        {
            if (accountReportResultList == null)
            {
                return;
            }

            var accountReportAccountList = accountReportResultList
                .Where(x => x.Operation != null)
                .Select(x => x.Operation)
                .Distinct()
                .ToList();

            foreach(var value in accountReportAccountList)
            {
                _communication.SendMessage(value);
            }
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

        public void ReportTransactionsWithCoin(List<AccountReportResult> accountReportResultList, string coin)
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

            foreach ( var group in accountReportAccountList )
            {
                if(group.Any(x => x.Coin == coin))
                {
                    _communication.SendMessage($"time: {group.Key}");
                    foreach (var element in group)
                    {
                        // Assuming you have a property 'SomeProperty' in the AccountReportResult class to print
                        _communication.SendMessage($"Coin: {element.Coin} | Operation: {element.Operation} | Change: {element.Change} | Account: {element.Account}");
                    }
                }
            }
        }

        private void GroupByValue(List<AccountReportResult> accountReportResultList, string value)
        {
            var groupList = accountReportResultList.Where(x => x.Operation.Contains(value.Replace("_", " ").Replace(".","").Replace("2.0", "2"))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });
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

using CsvReaderApp.Models;
using System.Drawing;
using System.Globalization;

namespace CsvReaderApp.Services
{
    public class BinanceReportService
    {
        public List<string> Operations { get; set; }

        public List<string> Coins { get; set; }

        public List<string> Accounts { get; set; }

        public List<Dictionary<string, List<AccountReportResult>>> BinanceReportResultsByAccount { get; set; }

        public BinanceReportService()
        {
            BinanceReportResultsByAccount = new List<Dictionary<string, List<AccountReportResult>>>();
        }

        public void Execute(List<BinanceReport> binanceReports)
        {
            foreach (BinanceReport binanceReport in binanceReports)
            {
                AddNewBinanceReport(binanceReport);
            }

            foreach (var result in BinanceReportResultsByAccount.Where(x => x.ContainsKey(AccountEnum.Spot.ToString())))
            {
                PrintAggregatedResultByOperationType("Deposit", result);

                PrintAggregatedResultByOperationType("Transaction_Related", result);

                PrintAggregatedResultByOperationType("Large_OTC_trading", result);

                PrintAggregatedResultByOperationType("Super_BNB_Mining", result);

                PrintAggregatedResultByOperationType("Buy", result);

                PrintAggregatedResultByOperationType("Sell", result);

                PrintAggregatedResultByOperationType("Fee", result);

                PrintAggregatedResultByOperationType("Referral_Kickback", result);

                PrintAggregatedResultByOperationType("POS_savings_interest", result);

                PrintAggregatedResultByOperationType("POS_savings_purchase", result);
            }


            Console.WriteLine();

            ProcessingBinanceReport();
        }

        private void PrintAggregatedResultByOperationType(string operationName, Dictionary<string, List<AccountReportResult>> binanceReportResultsByAccount)
        {
            if (!Enum.TryParse<OperationEnum>(operationName, out var operationValue))
            {
                return;
            }

            Console.WriteLine($"Total {operationName} By coin:");

            var result = binanceReportResultsByAccount[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(operationName.Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in result)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }
        }

        private void ProcessingBinanceReport()
        {
            throw new NotImplementedException();
        }

        private void AddNewBinanceReport(BinanceReport binanceReport)
        {
            if (binanceReport == null)
                return;

            AccountReportResult accountReportResult = new AccountReportResult();
            accountReportResult.Operation = binanceReport.Operation;
            accountReportResult.Coin = binanceReport.Coin;
            accountReportResult.Change = decimal.Parse(binanceReport.Change, NumberStyles.Float | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
            accountReportResult.Remark = binanceReport.Remark;

            var element = BinanceReportResultsByAccount?.FirstOrDefault(elem => elem.ContainsKey(binanceReport.Account));
            if (element != null)
            {
                element.Values.FirstOrDefault()?.Add(accountReportResult);
            }
            else
            {
                List<AccountReportResult> binanceReportResults = new List<AccountReportResult>();
                binanceReportResults.Add(accountReportResult);
                Dictionary<string, List<AccountReportResult>> keyValues = new Dictionary<string, List<AccountReportResult>>();
                keyValues.Add(binanceReport.Account, binanceReportResults);
                BinanceReportResultsByAccount?.Add(keyValues);
            }
        }

        private void PrintReport(string key, List<AccountReportResult> binanceReportResults)
        {
            Console.WriteLine($"For the {key} we have the following results:");
            foreach (var binanceReportResult in binanceReportResults)
            {
                Console.WriteLine($"| {binanceReportResult.Coin} | {binanceReportResult.Operation} | {binanceReportResult.Remark} | {binanceReportResult.Change} |");
            }
        }
    }
}

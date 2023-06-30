using CsvReaderApp.Models;
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

        public void Execute(List<BinanceReportEntry> binanceReports)
        {
            AddReportToResults(binanceReports);

            foreach (var result in BinanceReportResultsByAccount.Where(x => x.ContainsKey(AccountEnum.Spot.ToString())))
            {
                Console.WriteLine($"Total {OperationEnum.Deposit.ToString()} By coin:");

                var deposit = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Deposit.ToString())).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in deposit)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Transaction_Related.ToString()} By coin:");

                var transactions = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Transaction_Related.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in transactions)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Large_OTC_trading.ToString()} By coin:");

                var LargeOtcTradings = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Large_OTC_trading.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in LargeOtcTradings)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Super_BNB_Mining.ToString()} By coin:");

                var superBnbMining = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Super_BNB_Mining.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in superBnbMining)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Buy.ToString()} By coin:");

                var buy = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Buy.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in buy)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Sell.ToString()} By coin:");

                var sell = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Sell.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in sell)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Fee.ToString()} By coin:");

                var fees = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Fee.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in fees)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.Referral_Kickback.ToString()} By coin:");

                var referralKickback = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.Referral_Kickback.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in referralKickback)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.POS_savings_interest.ToString()} By coin:");

                var posSavingsInterest = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.POS_savings_interest.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in posSavingsInterest)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }

                Console.WriteLine($"Total {OperationEnum.POS_savings_purchase.ToString()} By coin:");

                var posSavingsPurchase = result[AccountEnum.Spot.ToString()].Where(x => x.Operation.Contains(OperationEnum.POS_savings_purchase.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

                foreach (var accountReport in posSavingsPurchase)
                {
                    Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
                }
            }


            Console.WriteLine();

            ProcessingBinanceReport();
        }

        private void AddReportToResults(List<BinanceReportEntry> binanceReports)
        {
            foreach (BinanceReportEntry binanceReport in binanceReports)
            {
                AddAccountReportResult(binanceReport);
            }
        }

        private void ProcessingBinanceReport()
        {
            throw new NotImplementedException();
        }

        private void AddAccountReportResult(BinanceReportEntry binanceReport)
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

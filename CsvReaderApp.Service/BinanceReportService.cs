using CsvReaderApp.Models;
using System.Globalization;

namespace CsvReaderApp.Services
{
    public class BinanceReportService
    {
        public List<string> Operations { get; set; }

        public List<string> Coins { get; set; }

        public List<string> Accounts { get; set; }

        public List<Dictionary<string, List<AccountReportResult>>> AccountReportResults { get; set; }

        public List<AccountReportResult> AggregatedReportResults { get; set; }

        public BinanceReportService()
        {
            AccountReportResults = new List<Dictionary<string, List<AccountReportResult>>>();
        }

        public void Execute(List<BinanceReport> binanceReports)
        {
            foreach (BinanceReport binanceReport in binanceReports)
            {
                AddNewBinanceReport(binanceReport);
            }

            if(AccountReportResults != null)
            {
                foreach (var binanceReportResult in AccountReportResults)
                {
                    ProcessAccountReport(binanceReportResult);
                }
            }
        }

        private void ProcessAccountReport(Dictionary<string, List<AccountReportResult>> accountReportResult)
        {
            Console.WriteLine($"Account: {accountReportResult.Keys}");

            var element = AccountReportResults?.FirstOrDefault(elem => elem.ContainsKey(accountReportResult.Keys));
            if (element != null)
            {
                element.Values.FirstOrDefault()?.Add(accountReportResult.Values);
            }
            else
            {
                List<AccountReportResult> binanceReportResults = new List<AccountReportResult>();
                binanceReportResults.Add(accountReportResult);
                Dictionary<string, List<AccountReportResult>> keyValues = new Dictionary<string, List<AccountReportResult>>();
                keyValues.Add(binanceReport.Account, binanceReportResults);
                AccountReportResults?.Add(keyValues);
            }

            throw new NotImplementedException();
        }

        private void AddNewBinanceReport(BinanceReport binanceReport)
        {
            if (binanceReport == null)
                return;

            AccountReportResult accountReportResult = new AccountReportResult();
            accountReportResult.Operation = binanceReport.Operation;
            accountReportResult.Coin = binanceReport.Coin;
            accountReportResult.Change = decimal.Parse(binanceReport.Change, NumberStyles.Float);
            accountReportResult.Remark = binanceReport.Remark;

            var element = AccountReportResults?.FirstOrDefault(elem => elem.ContainsKey(binanceReport.Account));
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
                AccountReportResults?.Add(keyValues);
            }
        }
    }
}

using CsvReaderApp.Models;
using System.Globalization;

namespace CsvReaderApp.Services
{
    public class BinanceReportService
    {
        public List<string> Operations { get; set; }

        public List<string> Coins { get; set; }

        public List<string> Accounts { get; set; }

        public List<Dictionary<string, List<BinanceReportResult>>> BinanceReportResults { get; set; }

        public BinanceReportService()
        {
            BinanceReportResults = new List<Dictionary<string, List<BinanceReportResult>>>();
        }

        public void Execute(List<BinanceReport> binanceReports)
        {
            foreach (BinanceReport binanceReport in binanceReports)
            {
                AddNewBinanceReport(binanceReport);
            }
        }

        private void AddNewBinanceReport(BinanceReport binanceReport)
        {
            if (binanceReport == null)
                return;

            BinanceReportResult binanceReportResult = new BinanceReportResult();
            binanceReportResult.Operation = binanceReport.Operation;
            binanceReportResult.Coin = binanceReport.Coin;
            binanceReportResult.Change = decimal.Parse(binanceReport.Change, NumberStyles.Float);
            binanceReportResult.Remark = binanceReport.Remark;

            var element = BinanceReportResults?.FirstOrDefault(elem => elem.ContainsKey(binanceReport.Account));
            if (element != null)
            {
                element.Values.FirstOrDefault()?.Add(binanceReportResult);
            }
            else
            {
                List<BinanceReportResult> binanceReportResults = new List<BinanceReportResult>();
                binanceReportResults.Add(binanceReportResult);
                Dictionary<string, List<BinanceReportResult>> keyValues = new Dictionary<string, List<BinanceReportResult>>();
                keyValues.Add(binanceReport.Account, binanceReportResults);
                BinanceReportResults?.Add(keyValues);
            }
        }

        private void PrintReport(string key, List<BinanceReportResult> binanceReportResults)
        {
            Console.WriteLine($"For the {key} we have the following results:");
            foreach (var binanceReportResult in binanceReportResults)
            {
                Console.WriteLine($"| {binanceReportResult.Coin} | {binanceReportResult.Operation} | {binanceReportResult.Remark} | {binanceReportResult.Change} |");
            }
        }
    }
}

using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;

namespace CsvReaderApp.Services
{
    public interface IAccountReportService
    {
        void Report(List<AccountReportResult> accountReportResultList);
    }
    public class AccountReportService : IAccountReportService
    {
        private readonly ICommunication _communication;
        public AccountReportService(ICommunication communication) 
        {
            _communication = communication;
        }

        public void Report(List<AccountReportResult> accountReportResultList)
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

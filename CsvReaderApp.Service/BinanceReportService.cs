using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using CsvReaderApp.Services.Utils;
using System.Collections.Generic;

namespace CsvReaderApp.Services
{
    public interface IBinanceReportService
    {
        void ReportSummary(List<AccountReportResult> accountReportResultList);
        void ReportTransactions(List<AccountReportResult> accountReportResultList);
        void ReportTransactionsWithCoin(List<AccountReportResult> accountReportResultList, string coin);
        void ReportTransactionsOutAndRelatedWithCoin(List<AccountReportResult> accountReportResultList, string coin);
        void ReportDistinctOperations(List<AccountReportResult> accountReportResultList);
        void ReportByCoin(List<AccountReportResult> accountReportResultList, string coin);
    }
    public class BinanceReportService : IBinanceReportService
    {
        private readonly ICommunication _communication;
        private readonly ITransactionService _transactionService;
        public BinanceReportService(ICommunication communication, ITransactionService transactionService)
        {
            _communication = communication;
            _transactionService = transactionService;
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

            foreach (var value in accountReportAccountList)
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

            var transactionEnums = Enum.GetNames(typeof(OperationEnum))
                .Where(e => e.ToLower().Contains("transaction")
                || e.ToLower().Contains("fee")
                || e.ToLower().Contains("referral")
                || e.ToLower().Contains("buy")
                || e.ToLower().Contains("sell")
                || e.ToLower().Contains("trading"))
                .ToList();
            transactionEnums = transactionEnums.Select(GetValueFromEnum).ToList();

            var accountReportAccountList = accountReportResultList.Where(x => x.Account == AccountEnum.Spot.ToString()
                && transactionEnums.Contains(GetValueFromEnum(x.Operation)))
                .GroupBy(x => x.DateTime)
                .ToList();
        }

        private string GetValueFromEnum(string enumValue)
        {
            if (string.IsNullOrEmpty(enumValue))
            {
                return string.Empty;
            }

            return enumValue
                .Replace("_", " ")
                .Replace(".", "")
                .Replace("2.0", "2");
        }

        public void ReportTransactionsWithCoin(List<AccountReportResult> accountReportResultList, string coin)
        {
            if (accountReportResultList == null)
            {
                return;
            }

            var transactionEnums = Enum.GetNames(typeof(OperationEnum))
                .Where(e => e.ToLower().Contains("transaction")
                || e.ToLower().Contains("fee")
                || e.ToLower().Contains("referral")
                || e.ToLower().Contains("buy")
                || e.ToLower().Contains("sell")
                || e.ToLower().Contains("trading"))
                .ToList();
            transactionEnums = transactionEnums.Select(GetValueFromEnum).ToList();

            var accountReportAccountList = accountReportResultList.Where(x => x.Account == AccountEnum.Spot.ToString()
                && transactionEnums.Contains(GetValueFromEnum(x.Operation)))
                .GroupBy(x => x.DateTime, new DateTimeWithMarginComparer())
                .ToList();

            foreach (var group in accountReportAccountList)
            {
                if (group.Any(x => x.Coin == coin))
                {
                    _communication.SendMessage($"time: {group.Key}");
                    foreach (var element in group)
                    {
                        _communication.SendMessage($"Coin: {element.Coin} | Operation: {element.Operation} | Change: {element.Change} | Account: {element.Account}");
                    }
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="accountReportResultList"></param>
        /// <param name="coin"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReportTransactionsOutAndRelatedWithCoin(List<AccountReportResult> accountReportResultList, string coin)
        {
            var transactionList = _transactionService.CreateTransactionResultList(accountReportResultList, coin);

            foreach (var transaction in transactionList)
            {
                _communication.SendMessage($"TransactionInId: {transaction.TransactionInId} | TransactionOutId: {transaction.TransactionOutId} | QuantityIn: {transaction.QuantityIn} | QuantityInMissing: {transaction.QuantityInMissing} | QuantityOut: {transaction.QuantityOut}");
            }
        }

        private void GroupByValue(List<AccountReportResult> accountReportResultList, string value)
        {
            var groupList = accountReportResultList.Where(x => x.Operation.Contains(value.Replace("_", " ").Replace(".", "").Replace("2.0", "2"))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });
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

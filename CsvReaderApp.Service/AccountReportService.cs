using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReaderApp.Services
{
    public interface IAccountReportService
    {
        void Report(List<AccountReportResult> accountReportResultList);
    }
    public class AccountReportService : IAccountReportService
    {
        public AccountReportService() { }

        public void Report(List<AccountReportResult> accountReportResultList) 
        {
            if (accountReportResultList == null)
            {
                return;
            }

            var accountReportAccountList = accountReportResultList.Where(x => x.Account == AccountEnum.Spot.ToString());
            
            Console.WriteLine($"Total {Binance.Models.OperationEnum.Deposit.ToString()} By coin:");

            var deposit = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Deposit.ToString())).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in deposit)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Transaction_Related.ToString()} By coin:");

            var transactions = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Transaction_Related.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in transactions)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Large_OTC_trading.ToString()} By coin:");

            var LargeOtcTradings = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Large_OTC_trading.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in LargeOtcTradings)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Super_BNB_Mining.ToString()} By coin:");

            var superBnbMining = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Super_BNB_Mining.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in superBnbMining)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Buy.ToString()} By coin:");

            var buy = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Buy.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in buy)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Sell.ToString()} By coin:");

            var sell = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Sell.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in sell)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Fee.ToString()} By coin:");

            var fees = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Fee.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in fees)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.Referral_Kickback.ToString()} By coin:");

            var referralKickback = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.Referral_Kickback.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in referralKickback)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.POS_savings_interest.ToString()} By coin:");

            var posSavingsInterest = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.POS_savings_interest.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in posSavingsInterest)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }

            Console.WriteLine($"Total {OperationEnum.POS_savings_purchase.ToString()} By coin:");

            var posSavingsPurchase = accountReportAccountList.Where(x => x.Operation.Contains(OperationEnum.POS_savings_purchase.ToString().Replace("_", " "))).GroupBy(c => c.Coin).Select(x => new { Coin = x.Key, Change = x.Sum(e => e.Change) });

            foreach (var accountReport in posSavingsPurchase)
            {
                Console.WriteLine($"| {accountReport.Coin} | {accountReport.Change} |");
            }
            
        }
    }
}

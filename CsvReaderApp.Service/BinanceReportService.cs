using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;
using CsvReaderApp.Services.Utils;

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
            //2021
            //Deposit, //In
            //Transaction_Related, //In & Out
            //Large_OTC_trading, //In & Out
            //Super_BNB_Mining,//In
            //--POS_savings_purchase,
            //Buy, //In
            //--Fee,
            //Referral_Kickback,//In
            //--Launchpool_Interest,
            //--POS_savings_interest,
            //--POS_savings_redemption,
            //Sell, //Out
            //--ETH_Staking,
            //--ETH_Staking_Reward,
            //--Savings_purchase, 
            //--Savings_Interest,
            //--Savings_Principal_redemption,
            //2022
            //Transaction_Buy,//In
            //Transaction_Spend, //Out
            //--Referral_Commission,
            //Transaction_Revenue, //In
            //Transaction_Sold,//Out
            //--Staking_Rewards,
            //--Simple_Earn_Flexible_Interest,
            //--Simple_Earn_Flexible_Subscription,
            //--Simple_Earn_Flexible_Redemption,
            //--Savings_Distribution,
            //--Staking_Purchase,
            //--ETH_2_Staking_Rewards,
            //Cash_Voucher_Distribution, //In
            //Distribution,//In
            //Fiat_Deposit,//In
            //Withdraw,//Out
            //--Small_Assets_Exchange_BNB,
            //transfer_out,//Out
            //transfer_in,//In
            //Main_and_Funding_Account_Transfer,//In
            //Binance_Card_Spending,//Out
            //Card_Cashback,//In
            //--Simple_Earn_Locked_Rewards,
            //--Stablecoins_AutoConversion, //TODO Stablecoins Auto-Conversion (fix)
            //--Simple_Earn_Locked_Subscription,
            //--Simple_Earn_Locked_Redemption,
            //Crypto_Box,//In
            //--BNB_Vault_Rewards,
            //--AutoInvest_Transaction

            if (accountReportResultList == null)
            {
                return;
            }

            List<TransactionResult> transactionList = new List<TransactionResult>();

            var transactionInEnums = Enum.GetNames(typeof(OperationEnum))
                .Where(
                    e => 
                    e == OperationEnum.Buy.ToString()
                    || e == OperationEnum.Transaction_Related.ToString()
                    || e == OperationEnum.Large_OTC_trading.ToString()
                    || e == OperationEnum.Sell.ToString()
                    || e == OperationEnum.Transaction_Spend.ToString()
                    || e == OperationEnum.Transaction_Buy.ToString()
                    || e == OperationEnum.Transaction_Revenue.ToString()
                    || e == OperationEnum.Transaction_Sold.ToString()
                    || e == OperationEnum.Cash_Voucher_Distribution.ToString()
                    || e == OperationEnum.transfer_in.ToString()
                    || e == OperationEnum.transfer_out.ToString()
                    || e == OperationEnum.Fiat_Deposit.ToString()
                    || e == OperationEnum.Main_and_Funding_Account_Transfer.ToString()
                    || e == OperationEnum.Crypto_Box.ToString()
                )
                .ToList();

            transactionInEnums = transactionInEnums.Select(GetValueFromEnum).ToList();

            var transactionInList = accountReportResultList.Where(x => transactionInEnums.Contains(GetValueFromEnum(x.Operation)) && x.Coin == coin && x.Change > 0)
                .ToList();

            foreach (var transactionIn in transactionInList)
            {
                _communication.SendMessage($"Coin: {transactionIn.Coin} | Operation: {transactionIn.Operation} | Change: {transactionIn.Change} | Account: {transactionIn.Account}");
            }

            var transactionOutEnums = Enum.GetNames(typeof(OperationEnum))
                .Where(
                    e =>
                    e == OperationEnum.Transaction_Related.ToString()
                    || e == OperationEnum.Large_OTC_trading.ToString()
                    || e == OperationEnum.Sell.ToString()
                    || e == OperationEnum.Buy.ToString()
                    || e == OperationEnum.Transaction_Buy.ToString()
                    || e == OperationEnum.Transaction_Spend.ToString()
                    || e == OperationEnum.Transaction_Revenue.ToString()
                    || e == OperationEnum.Transaction_Sold.ToString()
                    || e == OperationEnum.transfer_in.ToString()
                    || e == OperationEnum.transfer_out.ToString()
                )
                .ToList();

            transactionOutEnums = transactionInEnums.Select(GetValueFromEnum).ToList();

            var transactionOutList = accountReportResultList.Where(x => transactionOutEnums.Contains(GetValueFromEnum(x.Operation)) && x.Coin == coin && x.Change < 0)
                .ToList();
            //TODO
            foreach (var transactionOut in transactionOutList)
            {
                TransactionResult transaction = new TransactionResult();
                _communication.SendMessage($"Coin: {transactionOut.Coin} | Operation: {transactionOut.Operation} | Change: {transactionOut.Change} | Account: {transactionOut.Account}");
            }
        }

        private void FillTransaction(AccountReportResult transactionOut, List<AccountReportResult> transactionsInList, List<TransactionResult> transactionList)
        {
            int igoreIdFrom = 0;
            int highestTransactionInId = transactionList.Max(tr => tr.TransactionInId);
            bool useHighestTransactionInList = false;

            var transaction = transactionList.FirstOrDefault(x => x.TransactionInId == highestTransactionInId);

            if ( transaction != null && transaction.QuantityInMissing != 0m && !transaction.TransactionInFilled)
            {
                useHighestTransactionInList = true;
            }

            if (useHighestTransactionInList)
            {
                decimal transactionOutQty = transactionOut.Change;
                //decimal transactionOutQtyMissing = transactionOutQty;

                TransactionResult transactionResult = new TransactionResult();
                transactionResult.TransactionInId = highestTransactionInId;
                transaction.TransactionOutId = transactionOut.Id;
                transaction.QuantityIn = transaction.QuantityIn;
                //TODO
                if(transaction.QuantityInMissing >= transactionOutQty)
                {
                    transaction.QuantityOut = transactionOutQty;
                    transaction.QuantityInMissing -= transactionOutQty; //decrement qty missing
                    if(transaction.QuantityInMissing == 0m)
                    {
                        transaction.TransactionInFilled = true;
                    }
                }
                else if(transaction.QuantityInMissing < transactionOutQty)
                {
                    transaction.QuantityOut = transactionOutQty;
                    transactionOutQty = transaction.QuantityInMissing;
                    transaction.QuantityInMissing = 0m;
                    //TODO: need to go to next transaction
                }
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

using CsvReaderApp.Binance.Models;
using CsvReaderApp.Models;

namespace CsvReaderApp.Services
{
    public interface ITransactionService
    {
        List<TransactionResult> CreateTransactionResultList(List<AccountReportResult> accountReportResultList, string coin);
    }
    public class TransactionService : ITransactionService
    {
        public List<TransactionResult> CreateTransactionResultList(List<AccountReportResult> accountReportResultList, string coin)
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
                throw new ArgumentNullException(nameof(accountReportResultList));
            }
            if (coin == null)
            {
                throw new ArgumentNullException(nameof(coin));
            }

            List<AccountReportResult> IncreaseAccountReportListByCoin = CreateIncreaseAccountReportListByCoin(accountReportResultList, coin);
            List<AccountReportResult> DecreaseAccountReportListByCoin = CreateDecreaseAccountReportListByCoin(accountReportResultList, coin);

            List<TransactionResult> transactionList = new List<TransactionResult>();
            var options = new FillTransactionResultsOptions
            {
                IncreaseAccountReportListByCoin = IncreaseAccountReportListByCoin,
                TransactionList = transactionList
            };
            foreach (var transactionOut in DecreaseAccountReportListByCoin)
            {
                options.TransactionOut = transactionOut;
                FillTransactionResults(options);
            }


            return transactionList;
        }

        private void FillTransactionResults(FillTransactionResultsOptions options)
        {
            ValidateOptions(options);

            int highestTransactionInId = options.TransactionList?.Count > 0 ? options.TransactionList.Max(tr => tr.TransactionInId) : 0;
            bool useHighestTransactionInList = false;

            if (highestTransactionInId == 0)
            {
                highestTransactionInId = options.IncreaseAccountReportListByCoin.FirstOrDefault()?.Id ?? 0;
            }

            var transaction = options.TransactionList?.FirstOrDefault(x => x.TransactionInId == highestTransactionInId);

            if (transaction != null && transaction.QuantityInMissing != 0m && !transaction.TransactionInFilled)
            {
                useHighestTransactionInList = true;
            }
            else if (transaction != null && transaction.QuantityInMissing == 0m && transaction.TransactionInFilled)
            {
                highestTransactionInId = options.IncreaseAccountReportListByCoin.Where(t => t.Id > transaction.TransactionInId).OrderBy(t => t.Id).FirstOrDefault()?.Id ?? 0;
            }

            //int highestTransactionInId = GetHighestTransactionInId(options);
            //bool useHighestTransactionInList = ShouldUseHighestTransaction(options, highestTransactionInId);


            if (useHighestTransactionInList)
            {
                if(!transaction.TransactionInFilled)
                {
                    DecisionTransaction(options.IncreaseAccountReportListByCoin, options.TransactionList, options.TransactionOut, transaction);
                }
                else
                {
                    TransactionResult transactionResult = CreateTransactionResult(highestTransactionInId, options.TransactionOut.Id, transaction.QuantityIn);
                    DecisionTransaction(options.IncreaseAccountReportListByCoin, options.TransactionList, options.TransactionOut, transactionResult);
                }
            }
            else
            {
                var transactionInUnfilled = options.IncreaseAccountReportListByCoin.FirstOrDefault(x => x.Id == highestTransactionInId);

                if (transactionInUnfilled != null)
                {
                    TransactionResult transactionResult = CreateTransactionResult(transactionInUnfilled.Id, options.TransactionOut.Id, transactionInUnfilled.Change, transactionInUnfilled.Change);
                    DecisionTransaction(options.IncreaseAccountReportListByCoin, options.TransactionList, options.TransactionOut, transactionResult);
                }
            }
        }

        private void ValidateOptions(FillTransactionResultsOptions options)
        {
            if (options.TransactionOut == null)
            {
                throw new ArgumentNullException(nameof(options.TransactionOut));
            }
            if (options.IncreaseAccountReportListByCoin == null)
            {
                throw new ArgumentNullException(nameof(options.IncreaseAccountReportListByCoin));
            }
            if (options.TransactionList == null || options.TransactionList.Count == 0)
            {
                options.TransactionList = new List<TransactionResult>();
            }
        }

        private int GetHighestTransactionInId(FillTransactionResultsOptions options)
        {
            return options.TransactionList?.Count > 0 ? options.TransactionList.Max(tr => tr.TransactionInId) : 0;
        }

        private bool ShouldUseHighestTransaction(FillTransactionResultsOptions options, int highestTransactionInId)
        {
            if (highestTransactionInId == 0)
            {
                highestTransactionInId = options.IncreaseAccountReportListByCoin.FirstOrDefault()?.Id ?? 0;
            }

            var transaction = options.TransactionList?.FirstOrDefault(x => x.TransactionInId == highestTransactionInId);

            if (transaction != null && transaction.QuantityInMissing != 0m && !transaction.TransactionInFilled)
            {
                return true;
            }
            else if (transaction != null && transaction.QuantityInMissing == 0m && transaction.TransactionInFilled)
            {
                highestTransactionInId = options.IncreaseAccountReportListByCoin.Where(t => t.Id > transaction.TransactionInId).OrderBy(t => t.Id).FirstOrDefault()?.Id ?? 0;
            }

            return false;
        }

        private TransactionResult CreateTransactionResult(int transactionInId, int transactionOutId, decimal quantityIn, decimal quantityInMissing = 0m)
        {
            return new TransactionResult
            {
                TransactionInId = transactionInId,
                TransactionOutId = transactionOutId,
                QuantityIn = quantityIn,
                QuantityInMissing = quantityInMissing
            };
        }

        private void DecisionTransaction(List<AccountReportResult> transactionsInReportList, List<TransactionResult> transactionList, AccountReportResult transactionOut, TransactionResult transactionResult)
        {
            decimal transactionOutQty = transactionOut.Change;

            if (transactionResult.QuantityInMissing >= Math.Abs(transactionOutQty))
            {
                transactionResult.QuantityOut = transactionOutQty;
                transactionResult.QuantityInMissing -= Math.Abs(transactionOutQty);
                if (transactionResult.QuantityInMissing == 0m)
                {
                    transactionResult.TransactionInFilled = true;
                }
                transactionList.Add(transactionResult);
            }
            else if (transactionResult.QuantityInMissing < Math.Abs(transactionOutQty))
            {
                if(transactionOut.Change <0)
                {
                    transactionOut.Change += transactionResult.QuantityInMissing;
                }

                transactionResult.QuantityOut = transactionResult.QuantityInMissing;
                transactionResult.QuantityInMissing = 0m;
                transactionResult.TransactionInFilled = true;
                transactionList.Add(transactionResult);

                var options = new FillTransactionResultsOptions
                {
                    IncreaseAccountReportListByCoin = transactionsInReportList,
                    TransactionList = transactionList,
                    TransactionOut = transactionOut,
                };
                FillTransactionResults(options);
            }
        }


        private List<AccountReportResult> CreateIncreaseAccountReportListByCoin(List<AccountReportResult> accountReportResultList, string coin)
        {
            var transactionInEnums = Enum.GetNames(typeof(OperationEnum))
                .Where(
                    e =>
                    e == OperationEnum.Buy.ToString()
                    || e == OperationEnum.Deposit.ToString()
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

            return transactionInList;
        }

        private List<AccountReportResult> CreateDecreaseAccountReportListByCoin(List<AccountReportResult> accountReportResultList, string coin)
        {
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

            transactionOutEnums = transactionOutEnums.Select(GetValueFromEnum).ToList();

            var transactionOutList = accountReportResultList.Where(x => transactionOutEnums.Contains(GetValueFromEnum(x.Operation)) && x.Coin == coin && x.Change < 0)
                .ToList();

            return transactionOutList;
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
    }
}

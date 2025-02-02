﻿namespace CsvReaderApp.Binance.Models
{
    [Obsolete("to be migrated")]
    public enum OperationEnum
    {
        //2021
        Deposit,
        Transaction_Related,
        Large_OTC_trading,
        Super_BNB_Mining,
        POS_savings_purchase,
        Buy,
        Fee,
        Referral_Kickback,
        Launchpool_Interest,
        POS_savings_interest,
        POS_savings_redemption,
        Sell,
        ETH_Staking,
        ETH_Staking_Reward,
        Savings_purchase,
        Savings_Interest,
        Savings_Principal_redemption,
        //2022
        Transaction_Buy,
        Transaction_Spend,
        Referral_Commission,
        Transaction_Revenue,
        Transaction_Sold,
        Staking_Rewards,
        Simple_Earn_Flexible_Interest,
        Simple_Earn_Flexible_Subscription,
        Simple_Earn_Flexible_Redemption,
        Savings_Distribution,
        Staking_Purchase,
        ETH_2_Staking_Rewards, //TODO ETH 2.0 Staking Rewards (fix)
        Cash_Voucher_Distribution,
        Distribution,
        Fiat_Deposit,
        Withdraw,
        Small_Assets_Exchange_BNB,
        transfer_out,
        transfer_in,
        Main_and_Funding_Account_Transfer,
        Binance_Card_Spending,
        Card_Cashback,
        Simple_Earn_Locked_Rewards,
        Stablecoins_AutoConversion, //TODO Stablecoins Auto-Conversion (fix)
        Simple_Earn_Locked_Subscription,
        Simple_Earn_Locked_Redemption,
        Crypto_Box,
        BNB_Vault_Rewards,
        AutoInvest_Transaction
    }
}

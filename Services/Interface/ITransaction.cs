using System;
using System.Transactions;
using MiniBankApi.models;
using Transaction = MiniBankApi.models.Transaction;

namespace MiniBankApi.Services.Interface
{
    public interface ITransaction
    {
         Response CreateNewTransaction(Transaction transaction);
         Response FindTransactionByDate(DateTime date);
         Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin);
         Response MakeWithdrawal(string AccountNumber, decimal Amount, int TransactionPin);
         Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, int TransactionPin);
         
    }
}
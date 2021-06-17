using System;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniBankApi.Data;
using MiniBankApi.models;
using MiniBankApi.Services.Interface;
using MiniBankApi.Utils;

namespace MiniBankApi.Services.Implementations
{
    public class TransactionService : ITransaction
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionService> _logger;
        private readonly AppSettings _settings;
        private readonly IAccountService _accountService;
        private static string _bankSettlementAccount;

        public TransactionService(ApplicationDbContext context,
                                ILogger<TransactionService> logger,
                                IOptions<AppSettings> settings,
                                IAccountService accountService)
        {
            _settings = settings.Value;
            _accountService = accountService;
            _context = context;
            _logger = logger;
        }

        public Response CreateNewTransaction(Transaction transaction)
        {
            Response response = new Response();
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            response.ReponseCode = "00";
            response.ResponseMessage = "Transaction created successfully";
            response.data = null;

            return response;
        }

        public Response FindTransactionByDate(DateTime date)
        {
            Response response = new Response();
            var transaction = _context.Transactions.Where(x => x.TransactionDate == date).ToList();
            
            response.ReponseCode = "00";
            response.ResponseMessage = "Transaction created successfully";
            response.data = transaction;

            return response;
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            Response response = new Response();

             Account sourceAccount;
             Account destinationAccount;
             Transaction transaction = new Transaction();

            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);

            if (authUser == null) throw new ApplicationException("Invalid credentials");

            try
            {
                sourceAccount = _accountService.GetByAccountNumber(_bankSettlementAccount);
                destinationAccount = _accountService.GetByAccountNumber(AccountNumber);

                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;

                if((_context.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                    _context.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) 
                    {
                        transaction.TransactionStatus = TranStatus.Success;
                        response.ReponseCode = "00";
                        response.ResponseMessage = "Transaction Successful";
                        response.data = null;
                    } else 
                    {
                        transaction.TransactionStatus = TranStatus.Failed;
                        response.ReponseCode = "02";
                        response.ResponseMessage = "Transaction Failed";
                        response.data = null;
                    }
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occured... {exp.Message}");

            }

            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _bankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW Transaction FROM SOURCE {JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT=> {JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON DATE => {transaction.TransactionDate} TRAN_TYPE =>  {transaction.TransactionType} TRAN_STATUS => {transaction.TransactionStatus}";



            _context.Transactions.Add(transaction);
            _context.SaveChanges();


            return response;

        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, int TransactionPin)
        {
            throw new NotImplementedException();
        }

        public Response MakeWithdrawal(string AccountNumber, decimal Amount, int TransactionPin)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using MiniBankApi.models;

namespace MiniBankApi.Services
{
    public interface IAccountService
    {
         Account Authenticate(string AccountNumber, string AccountPin);
         IEnumerable<Account> GetAllAccounts();

         Account Create(Account account, string Pin, string ConfirmPin);

         void Update(Account account, string Pin = null);
         void Delete(int Id);
         Account GetById(int Id);

         Account GetByAccountNumber(string AccountNumber);
    }
}
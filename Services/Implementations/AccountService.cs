using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using MiniBankApi.Data;
using MiniBankApi.models;

namespace MiniBankApi.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Account Authenticate(string AccountNumber, string AccountPin)
        {
            var account = _context.Accounts.Where(x => x.AccountNumber == AccountNumber).SingleOrDefault();

            if (account == null) return null;

            if (!verifyPinHash(AccountPin, account.PinHash, account.PinSalt)) return null;

            return account;
        }

        private static bool verifyPinHash(string Pin,byte[] PinHash, byte[] PinSalt) 
        {
            if (String.IsNullOrWhiteSpace(Pin)) throw new ArgumentException("Pin");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(PinSalt))
            {
                var computedPinHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pin));

                for (int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != PinHash[i]) return false;
                }
            }

            return true;

        }

        public Account Create(Account account, string Pin, string ConfirmPin)
        {
            if (_context.Accounts.Any(x => x.Email == account.Email)) throw new ApplicationException("An Account Alreadt Exist");

            if (!Pin.Equals(ConfirmPin)) throw new ArgumentException("Pin does not match");

            byte[] pinHash, pinSalt;
            CreatePinHash(Pin, out pinHash, out pinSalt);

            account.PinHash = pinHash;
            account.PinSalt = pinSalt;
            account.AccountName = account.FirstName+" "+account.LastName;

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        private static void CreatePinHash(string pin, out byte[] pinHash, out byte[] pinSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));
            }
        }

        public void Delete(int Id)
        {
            var account = _context.Accounts.Find(Id);
            if (account != null) 
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _context.Accounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefault();

            if (account == null) return null;

            return account;
        }

        public Account GetById(int Id)
        {
            var account = _context.Accounts.Where(x => x.Id == Id).FirstOrDefault();
            if (account == null) return null;

            return account;
        }

        public void Update(Account account, string Pin = null)
        {
            var accountToUpdate = _context.Accounts.FirstOrDefault(x => x.AccountNumber == account.AccountNumber);

            if (accountToUpdate == null) throw new ApplicationException("Account does not exist");

            if (!string.IsNullOrWhiteSpace(account.Email)) {
                if(_context.Accounts.Any(x => x.Email == account.Email)) new ApplicationException("This Email "+account.Email+" already exist");
                accountToUpdate.Email = account.Email;
            }

            if (!string.IsNullOrWhiteSpace(account.PhoneNumber)) {
                if(_context.Accounts.Any(x => x.PhoneNumber == account.PhoneNumber)) new ApplicationException("This Email "+account.PhoneNumber+" already exist");
                accountToUpdate.PhoneNumber = account.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(Pin)) {

                byte[] pinHash, pinSalt;
                CreatePinHash(Pin, out pinHash,out pinSalt);
                accountToUpdate.PinHash = pinHash;
                accountToUpdate.PinSalt = pinSalt;
            }

            accountToUpdate.UpdatedAt = DateTime.Now;
            _context.Accounts.Update(accountToUpdate);
            _context.SaveChanges();
        }
    }
}
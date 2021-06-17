using System.Security.Cryptography;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniBankApi.models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        Random rand = new Random();

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountNumber { get; set; }
        [JsonIgnore]
        public byte[] PinHash {get; set; }
        [JsonIgnore]
        public byte[] PinSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Account()
        {
            AccountNumber = Convert.ToString(
                (long)Math.Floor(rand.NextDouble() * 9_000_000_000L + 1_000_000_000L));
        }


    }

    public enum AccountType
    {
        Savings,
        Current,
        Corporate
    }
}

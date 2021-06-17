using System;
using System.ComponentModel.DataAnnotations;
using MiniBankApi.models;

namespace MiniBankApi.Dto
{
    public class GetAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using MiniBankApi.models;

namespace MiniBankApi.Dto
{
    public class RegisterNewAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        // public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        // public string AccountNumber { get; set; }

        // public byte[] PinHash {get; set; }
        // public byte[] PinSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [RegularExpression(@"\d{4}[0-9]$", ErrorMessage = "Pin must be 4 digits")]
        public string Pin { get; set; }
        [Required]
        [Compare("Pin", ErrorMessage = "Pin does not match")]
        public string ConfirmPin { get; set; }

}
}
using System;
using System.ComponentModel.DataAnnotations;

namespace MiniBankApi.Dto
{
    public class UpdateAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        // public decimal CurrentAccountBalance { get; set; }
        public string AccountNumber { get; set; }

        public DateTime UpdatedAt { get; set; }
        [Required]
        public string Pin { get; set; }
        [Required]
        [Compare("Pin", ErrorMessage = "Pin does not match")]
        public string ConfirmPin { get; set; }

    }
}
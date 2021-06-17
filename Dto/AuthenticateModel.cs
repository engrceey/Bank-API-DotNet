using System.ComponentModel.DataAnnotations;

namespace MiniBankApi.Dto
{
    public class AuthenticateModel
    {
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string Pin { get; set; }
    }
}
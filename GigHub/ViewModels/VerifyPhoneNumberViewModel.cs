using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "C�digo")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "N�mero de tel�fono")]
        public string PhoneNumber { get; set; }
    }
}
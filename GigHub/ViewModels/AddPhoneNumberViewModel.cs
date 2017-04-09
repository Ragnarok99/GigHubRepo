using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "N�mero de tel�fono")]
        public string Number { get; set; }
    }
}
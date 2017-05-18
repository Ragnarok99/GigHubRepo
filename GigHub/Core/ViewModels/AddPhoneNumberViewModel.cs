using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "N�mero de tel�fono")]
        public string Number { get; set; }
    }
}
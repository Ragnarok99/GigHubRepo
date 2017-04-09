using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string Number { get; set; }
    }
}
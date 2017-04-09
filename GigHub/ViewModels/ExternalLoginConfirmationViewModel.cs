using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo electr�nico")]
        public string Email { get; set; }
    }
}
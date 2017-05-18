using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}
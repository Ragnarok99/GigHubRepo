using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo electr�nico")]
        public string Email { get; set; }
    }
}
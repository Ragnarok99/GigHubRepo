using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electr�nico")]
        public string Email { get; set; }
    }
}

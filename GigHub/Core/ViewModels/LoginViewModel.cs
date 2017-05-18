using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Correo electr�nico")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contrase�a")]
        public string Password { get; set; }

        [Display(Name = "�Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }
}
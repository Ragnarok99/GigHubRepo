using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contrase�a actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contrase�a nueva")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contrase�a nueva")]
        [Compare("NewPassword", ErrorMessage = "La contrase�a nueva y la contrase�a de confirmaci�n no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
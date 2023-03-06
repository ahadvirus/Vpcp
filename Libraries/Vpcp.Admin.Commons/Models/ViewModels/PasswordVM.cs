using System.ComponentModel.DataAnnotations;

namespace Vpcp.Admin.Commons.Models.ViewModels
{
    public class PasswordVM
    {
        public PasswordVM()
        {
            Passowrd = string.Empty;
            ConfirmPassword = string.Empty;
        }

        [Required]
        public string Passowrd { get; set; }
        [Required]
        [Compare(nameof(Passowrd))]
        public string ConfirmPassword { get; set; }
    }
}

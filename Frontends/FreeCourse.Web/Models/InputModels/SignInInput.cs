using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.InputModels
{
    public class SignInInput
    {
        [Required(ErrorMessage ="Email gereklidir.")]
        [Display(Name = "Email adresiniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [Display(Name = "Sifreniz.")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla.")]
        public bool IsRemember { get; set; }
    }
}

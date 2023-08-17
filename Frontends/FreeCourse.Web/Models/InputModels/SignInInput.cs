using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.InputModels
{
    public class SignInInput
    {
        [Display(Name = "Email adresiniz.")]
        public string Email { get; set; }
        [Display(Name = "Sifreniz.")]
        public string Password { get; set; }
        [Display(Name = "Beni hatırla.")]
        public bool IsRemember { get; set; }
    }
}

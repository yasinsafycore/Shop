using Shop.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Account
{
    public class RegisterUserViewModel : GoogleRecaptchaViewModel
    {
        [MaxLength(200)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }

    public enum RegisterUserResult
    {
        Success,
        EmailExist
    }

    //---------------------------------------------------------------

    public class LoginUserViewModel : GoogleRecaptchaViewModel
    {
        [MaxLength(200)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }

    public enum LoginUserResult
    {
        Success,
        NotActive,
        NotFound,
        IsBan
    }

    //----------------------------------------------------------------

    public class ForgotPasswordViewModel : GoogleRecaptchaViewModel
    {
        [MaxLength(200)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }

    public enum ForgotPasswordResult
    {
        Success,
        NotFound,
        IsBan
    }

    //----------------------------------------------------------------

    public class ResetPasswordViewModel : GoogleRecaptchaViewModel
    {
        public string ActivationCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        [Compare("Password")]
        public string RePassword { get; set; }

    }

    public enum ResetPasswordResult
    {
        Success,
        NotFound,
        IsBan
    }
}

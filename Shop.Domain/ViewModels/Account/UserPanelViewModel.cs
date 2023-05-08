using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Account
{
    public class EditPanelViewModel
    {
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
    }

    public enum EditPanelResult
    {
        Success,
        IsBan
    }

    //---------------------------------------------------------------

    public class ChangePasswordViewModel
    {
        [Required]
        [MaxLength(100)]
        public string CurrentPassword { get; set; }

        [Required]
        [MaxLength(100)]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(100)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }

}

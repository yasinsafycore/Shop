using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.Dashboard
{
    public class AdminProfileViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }
    }

    //---------------------------------------------------------------

    public class EditProfileViewModel
    {
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }
    }

    //---------------------------------------------------------------

    public class EditImgProfileViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

}

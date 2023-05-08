using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.SiteUI
{
    public class HeaderBannerViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }
    }

    //---------------------------------------------------------------

    public class EditBannerViewModel
    {

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }
    }

    //---------------------------------------------------------------

    public class EditImgBannerViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

}

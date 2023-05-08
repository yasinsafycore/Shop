using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Domain.ViewModels.Admin.Blog
{
    public class BlogeViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Content { get; set; }
    }

    //---------------------------------------------------------------

    public class EditBlogeViewModel
    {
        [Required]
        [MaxLength(200, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Content { get; set; }
    }

    //---------------------------------------------------------------

    public class EditImgBlogeViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

}

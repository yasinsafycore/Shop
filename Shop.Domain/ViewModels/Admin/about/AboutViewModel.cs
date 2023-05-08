using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.about
{
    public class AboutUsViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string SubTitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string ButtonTitle { get; set; }

        [Required]
        public string ButtonLink { get; set; }
    }

    //---------------------------------------------------------------

    public class EditAboutUsViewModel
    {
        [Required]
        [MaxLength(200, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string SubTitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string ButtonTitle { get; set; }

        [Required]
        public string ButtonLink { get; set; }
    }

    //---------------------------------------------------------------

    public class EditImgAboutUsViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

    //---------------------------------------------------------------

    public class TeamViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string Job { get; set; }

    }

    //---------------------------------------------------------------

    public class EditTeamViewModel
    {
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string Job { get; set; }

    }

    //---------------------------------------------------------------

    public class EditImgTeamViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.Setting.General
{
    public class AdminLogoViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

    //---------------------------------------------------------------

    public class SiteLogoViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        public string SiteDescription { get; set; }
    }

    //---------------------------------------------------------------

    public class SiteImgViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

    //---------------------------------------------------------------

    public class SiteDescriptionViewModel
    {
        public string SiteDescription { get; set; }

    }

    //---------------------------------------------------------------

    public class CopyRightViewModel
    {
        public string AdminText { get; set; }

        public string SiteText { get; set; }
    }

}

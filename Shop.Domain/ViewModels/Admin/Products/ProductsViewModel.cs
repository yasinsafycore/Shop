using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.Products
{
    public class AddProductsViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        [MaxLength(600, ErrorMessage = "The text is more than required")]
        public string SubTitle { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "The ProductInventory field is required.")]
        public double NumberOfProducts { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Categories")]
        [Required(ErrorMessage = "The Categories field is required.")]
        public int CategoriesId { get; set; }
    }

    //---------------------------------------------------------------

    public class EditProductsViewModel
    {
        [Required]
        [MaxLength(300, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        [MaxLength(600, ErrorMessage = "The text is more than required")]
        public string SubTitle { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        public double NumberOfProducts { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Categories")]
        public int CategoriesId { get; set; }
    }

    //---------------------------------------------------------------

    public class EditImgProductsViewModel
    {
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }

}

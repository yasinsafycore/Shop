using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.Products
{
    public class CategoriesViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }
    }

    //---------------------------------------------------------------

    public class EditCategoriesViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }
    }

}

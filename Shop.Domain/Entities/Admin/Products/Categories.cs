using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Domain.Entities.Admin.Products
{
    public class Categories : BaseEntity
    {
        #region Properties

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        #endregion

        #region Relations

        public ICollection<Product> Products { get; set; }

        #endregion
    }
}

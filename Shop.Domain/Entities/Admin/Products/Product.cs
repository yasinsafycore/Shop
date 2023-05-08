using Shop.Domain.Entities.Common;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.Entities.Shop.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.Products
{
    public class Product : BaseEntity
    {
        #region Properties

        public string Img { get; set; }

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

        public int CategoriesId { get; set; }

        #endregion

        #region Relations

        public Categories Categories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Comment> Comments { get; set; }

        #endregion
    }
}

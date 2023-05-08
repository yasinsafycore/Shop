using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Shop.Orders
{
    public class OrderDetail : BaseEntity
    {
        #region Properties

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Count { get; set; }

        [Required]
        public double Price { get; set; }

        #endregion

        #region Relations

        public Order Order { get; set; }
        public Product Product { get; set; }

        #endregion
    }
}

using Shop.Domain.Entities.Account;
using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Shop.Orders
{
    public class Order : BaseEntity
    {
        #region Properties

        [Required]
        public int UserId { get; set; }

        [Required]
        public double OrderSum { get; set; }

        public bool IsFinaly { get; set; }

        public bool Posted { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}

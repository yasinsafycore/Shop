using Shop.Domain.Entities.Common;
using Shop.Domain.Entities.Shop.Account;
using Shop.Domain.Entities.Shop.Orders;
using Shop.Domain.Entities.Shop.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Domain.Entities.Account
{
    public class User : BaseEntity
    {
        #region Properties

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public bool IsEmailActive { get; set; }

        public string EmailActivationCode { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBan { get; set; }

        #endregion

        #region Relations

        public ICollection<UserPermission> UserPermissions { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }

        #endregion
    }
}

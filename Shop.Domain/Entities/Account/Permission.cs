using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Domain.Entities.Account
{
    public class Permission
    {
        #region Proppeties

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        #endregion

        #region Relations

        public ICollection<UserPermission> UserPermissions { get; set; }

        #endregion
    }
}

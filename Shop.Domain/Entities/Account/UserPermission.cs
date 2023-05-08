using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Account
{
    public class UserPermission
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PermissionId { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }

        public Permission Permission { get; set; }

        #endregion
    }
}

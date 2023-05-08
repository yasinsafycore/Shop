using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.Setting.Footer
{
    public class FooterLabel : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Relations

        public ICollection<FooterLink> FooterLinks { get; set; }

        #endregion
    }
}

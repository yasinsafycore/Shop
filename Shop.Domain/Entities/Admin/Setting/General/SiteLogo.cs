using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.Setting.General
{
    public class SiteLogo : BaseEntity
    {
        #region Properties

        public string Img { get; set; }

        public string SiteDescription { get; set; }

        #endregion
    }
}

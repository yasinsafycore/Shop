using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.Setting.General
{
    public class CopyRight : BaseEntity
    {
        #region Properties

        public string AdminText { get; set; }

        public string SiteText { get; set; }

        #endregion
    }
}

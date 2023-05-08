using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.Setting.Footer
{
    public class FooterLink : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public string Url { get; set; }

        public int FooterLabelId { get; set; }

        #endregion

        #region Relations

        public FooterLabel FooterLabel { get; set; }

        #endregion
    }
}

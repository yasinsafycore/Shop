using Shop.Domain.Entities.Admin.Blog;
using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Domain.Entities.Shop.Site
{
    public class BlogComment : BaseEntity
    {
        #region Properties

        public string Content { get; set; }

        public int WeBlogeId { get; set; }

        #endregion

        #region Relations

        public WeBloge WeBloge { get; set; }

        #endregion
    }
}

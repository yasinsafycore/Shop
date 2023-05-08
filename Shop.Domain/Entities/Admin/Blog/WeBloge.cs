using Shop.Domain.Entities.Admin.Products;
using Shop.Domain.Entities.Common;
using Shop.Domain.Entities.Shop.Site;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shop.Domain.Entities.Admin.Blog
{
    public class WeBloge : BaseEntity
    {
        #region Properties

        public string Img { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }
        
        [Required]
        public string Content { get; set; }

        #endregion

        #region Relations

        public ICollection<BlogComment> BlogComments { get; set; }

        #endregion
    }
}

using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.SiteUI
{
    public class MiddleBanner : BaseEntity
    {
        #region Properties

        public string Img { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "The text is more than required")]
        public string SmallDescription { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The text is more than required")]
        public string ButtonTitle { get; set; }

        [Required]
        public string ButtonLink { get; set; }

        #endregion
    }
}

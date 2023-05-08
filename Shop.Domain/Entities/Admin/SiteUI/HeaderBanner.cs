﻿using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.SiteUI
{
    public class HeaderBanner : BaseEntity
    {
        #region Properties

        public string Img { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The text is more than required")]
        public string Title { get; set; }

        [Required]
        public string Link { get; set; }

        #endregion
    }
}

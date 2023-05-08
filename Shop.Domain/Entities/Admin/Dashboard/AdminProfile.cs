﻿using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Admin.Dashboard
{
    public class AdminProfile : BaseEntity
    {
        public string Img { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }
    }
}

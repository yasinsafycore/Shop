using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Shop.Site
{
    public class ContactUs : BaseEntity
    {
        [MaxLength(200, ErrorMessage = "The text is more than required")]
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "The text is more than required")]
        [Required]
        public string Subject { get; set; }

        public string Message { get; set; }
    }
}

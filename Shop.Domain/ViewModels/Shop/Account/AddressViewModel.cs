using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Shop.Account
{
    public class AddressViewModel
    {
        #region Properties

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string Country { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string City { get; set; }

        [MaxLength(500, ErrorMessage = "The text is more than required")]
        [Required]
        public string StreetAddress { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string Postcode { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Phone]
        [Required]
        public string Phone { get; set; }

        public string? OrderNotes { get; set; }

        public int UserId { get; set; }

        #endregion
    }

    //---------------------------------------------------------------

    public class EditAddressViewModel
    {
        #region Properties

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string Country { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string City { get; set; }

        [MaxLength(500, ErrorMessage = "The text is more than required")]
        [Required]
        public string StreetAddress { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Required]
        public string Postcode { get; set; }

        [MaxLength(100, ErrorMessage = "The text is more than required")]
        [Phone]
        [Required]
        public string Phone { get; set; }

        public string? OrderNotes { get; set; }

        public int UserId { get; set; }

        #endregion
    }
}

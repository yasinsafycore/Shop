using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Shop.Order
{
    public class OrderViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public double OrderSum { get; set; }

        public bool IsFinaly { get; set; }
    }

    public enum OrderResult
    {
        Success,
        IsBan,
        NotFound
    }

    //---------------------------------------------------------------

    public class PostedViewModel
    {
        public bool Posted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.Setting.Footer
{
    public class FooterLabelViewModel
    {
        public string Title { get; set; }
    }

    //---------------------------------------------------------------

    public class FooterLinkViewModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public int FooterLabelId { get; set; }
    }
}

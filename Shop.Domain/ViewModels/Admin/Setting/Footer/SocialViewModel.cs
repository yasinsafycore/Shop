using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Admin.Setting.Footer
{
    public class SocialViewModel
    {
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string Instagram { get; set; }
    }

    //---------------------------------------------------------------

    public class EditSocialViewModel
    {
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string Instagram { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class CommonAbstract
    {
        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }
    }
}
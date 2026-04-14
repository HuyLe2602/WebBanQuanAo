using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string seoTitle { get; set; }

        public string seoDescription { get; set; }

        public string seoKeywords { get; set; }
        
        public string Position { get; set; }
    }
}
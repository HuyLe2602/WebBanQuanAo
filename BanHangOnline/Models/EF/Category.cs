using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages.Instrumentation;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category() 
        { 
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Khong được để trống tên danh mục")]
        [StringLength(250)]
        public string Title { get; set; }

        public string Description { get; set; }
        [StringLength(250)]
        public string seoTitle { get; set; }
        [StringLength(250)]
        public string seoDescription { get; set; }
        [StringLength(250)]
        public string seoKeywords { get; set; }
        
        public string Position { get; set; }

        public ICollection<News> News { get; set; }

        public ICollection<Posts> Posts { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangOnline.Models.EF
{
    [Table("tb_News")]
    public class News: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Ten tin tuc khong duoc de trong")]
        [StringLength(150)]
        public string Title { get; set; }
        
        public string Alias { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string Detail { get; set; }
        
        public string Image { get; set; }

        public string CategoryId { get; set; }

        // whether the news item is active/published
        public bool IsActive { get; set; }

        // mark as featured / important
        public bool IsFeatured { get; set; }

        // additional settings or tags (comma separated)
        [StringLength(250)]
        public string Settings { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string SeoKeywords { get; set; }

        public virtual Category Category { get; set; }
    }
}
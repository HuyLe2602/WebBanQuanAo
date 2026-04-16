using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Posts")]
    public class Posts: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Detail { get; set; }

        public string Image { get; set; }

        public string CategoryId { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string SeoKeywords { get; set; }
        // Navigation to a parent post (if used). Renamed to avoid member name == type error.
        public virtual Posts ParentPost { get; set; }
    }
}
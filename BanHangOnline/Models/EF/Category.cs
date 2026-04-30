using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category()
        {
            this.News = new HashSet<News>();
            this.Posts = new HashSet<Posts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống tên danh mục")]
        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string seoTitle { get; set; }

        [StringLength(500)]
        public string seoDescription { get; set; }

        [StringLength(250)]
        public string seoKeywords { get; set; }

        public int Position { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<News> News { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
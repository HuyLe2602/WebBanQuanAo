using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory : CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
            this.Children = new HashSet<ProductCategory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục sản phẩm không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Icon { get; set; }

        public int? ParentId { get; set; }   // Danh mục cha

        public int Position { get; set; }    // Thứ tự hiển thị

        public bool IsActive { get; set; }   // Hiển thị / Ẩn

        [ForeignKey("ParentId")]
        public virtual ProductCategory Parent { get; set; }

        public virtual ICollection<ProductCategory> Children { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
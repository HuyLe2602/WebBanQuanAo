using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
        [Table("tb_Product")]
    public class Product: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        public string Alias { get; set; }

        public string ProductCode { get; set; }

        public string Description { get; set; }

        public string Detail { get; set; }

        public string Image { get; set; }
        public string Price { get; set; }

        public string PriceSale { get; set; }

        public string Quantity { get; set; }

        public bool IsHome { get; set; }

        public bool IsSale { get; set; }

        public bool IsFeature { get; set; }

        public bool IsHot { get; set; }

        public string ProductCategoryId { get; set; }

        public string SeoTitle { get; set; }

        public string SeoDescription { get; set; }

        public string SeoKeywords { get; set; }


        public virtual ProductCategory ProductCategory { get; set; }
    }
}
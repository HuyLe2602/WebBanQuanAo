using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanHangOnline.Models.EF
{
    [Table("tb_News")]
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        public string Description { get; set; }

        public string Detail { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public bool IsActive { get; set; }

        public bool IsHome { get; set; }

        public bool IsHot { get; set; }

        public int Position { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
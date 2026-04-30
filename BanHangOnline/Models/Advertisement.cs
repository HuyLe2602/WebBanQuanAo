using System;
using System.ComponentModel.DataAnnotations;

namespace BanHangOnline.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
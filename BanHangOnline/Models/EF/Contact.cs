using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models.EF
{
    [Table("tb_Contact")]
    public class Contact: CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
        [StringLength(150, ErrorMessage = "Tên của bạn không được vượt quá 150 ký tự")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }
        [StringLength(4000)]
        public string Message { get; set; }

        public bool IsRead { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Nhom6_DoAn_.Models
{
    public class DatVe
    {
        [Key]
        [StringLength(20)]
        public string MaVe { get; set; } // MAVE: Mã vé

        [StringLength(20)]
        public string TrangThai { get; set; } // TRANGTHAI: Trạng thái vé

        public int GiaVe { get; set; } // GIAVE: Giá vé

        [Required]
        
        public string Email{ get; set; } // Email

        [Required]
        [StringLength(20)]
        public string MaGhe { get; set; } // MAGHE: Mã ghế

        [Required]
        [StringLength(20)]
        public string MaSuat { get; set; } // MASUAT: Mã suất chiếu
    }
}

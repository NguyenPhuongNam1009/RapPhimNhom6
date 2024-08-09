using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nhom6_DoAn_.Models
{
    public class SuatChieu
    {
        [Key]
        public string MaSuat { get; set; }
        public DateTime NgayChieu { get; set; }
        public TimeSpan KhungGio { get; set; }
        public string MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
    }
}
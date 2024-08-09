using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nhom6_DoAn_.Models
{
    public class Phim
    {
        [Key]
        public string MaPhim { get; set; }

        public string TenPhim { get; set; }

        public string AnhBia { get; set; }

        public string TrailerUrl { get; set; }

        public string DaoDien { get; set; }

        public string DienVien { get; set; }

        public string TheLoai { get; set; }

        public int ThoiLuong { get; set; }

        public string NgonNgu { get; set; }

        public DateTime KhoiChieu { get; set; }
    }
}
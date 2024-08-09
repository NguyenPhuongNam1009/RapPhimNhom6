using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom6_DoAn_.Models
{
    public class MovieInfo
    {
        public string MaPhim { get; set; }
        public string TenPhim { get; set; }

        public string AnhBia { get; set; }
        public string NgayChieu { get; set; }
        public string KhungGio { get; set; }

        public string NgayChieuFormatted { get; set; }

        public string PhongChieu { get; set; }
    }
}
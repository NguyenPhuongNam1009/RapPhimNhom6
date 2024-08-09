using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nhom6_DoAn_.Models
{
    public class Ghe
    {
        [Key]
        public string MaGhe { get; set; }
        public string Day { get; set; }
    }
}
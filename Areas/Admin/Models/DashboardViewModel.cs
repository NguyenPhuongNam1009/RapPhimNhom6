using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Nhom6_DoAn_.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        [Key]
        public string Dbkey { get; set; }
        public int TotalMovies { get; set; }
        public int TotalShows { get; set; }

        public int TotalUser { get; set; }

        public int TotalTicketBooked { get; set; }
    }
}
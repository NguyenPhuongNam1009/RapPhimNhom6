using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom6_DoAn_.Areas.Admin.Models;

namespace Nhom6_DoAn_.Areas.Admin.Controllers
{
    
    public class AdminController : Controller
    {

        private RAPPHIMEntities db = new RAPPHIMEntities();

        public ActionResult Index()
        {
           
            var dashboardData = new DashboardViewModel
            {
                TotalMovies = db.PHIMs.Count(),
                TotalShows = db.SUATCHIEUx.Count(),
                TotalUser = db.Tbl_Users.Count(),
                TotalTicketBooked = db.DATVEs.Count(),
                // Thêm dữ liệu khác bạn muốn hiển thị trên dashboard
            };

            return View(dashboardData);
        }

        public JsonResult GetChartData()
        {
            // Giả sử bạn có một bảng dữ liệu tên là `YourTable`
            var data = db.SUATCHIEUx
                .GroupBy(x => x.MAPHIM) // Nhóm theo loại hoặc nhóm cần thiết
                .Select(g => new ChartData
                {
                    Label = g.Key,
                    Value = g.Count()
                }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetChartDataUsers()
        {
            // Giả sử bạn có một bảng dữ liệu tên là `YourTable`
            var data = db.DATVEs
                .GroupBy(x => x.MASUAT) // Nhóm theo loại hoặc nhóm cần thiết
                .Select(g => new ChartData
                {
                    Label = g.Key,
                    Value = g.Count()
                }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
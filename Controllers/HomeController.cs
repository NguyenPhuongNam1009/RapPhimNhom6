using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom6_DoAn_.Models;

namespace Nhom6_DoAn_.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private PhimRepository repository = new PhimRepository();


        [HttpGet]
        public ActionResult SearchPhim(string query)
        {
            var result = repository.SearchPhims(query);
            return PartialView("_PhimList", result);
        }
        public ActionResult Index()
        {
            var phims = repository.GetAllPhims();


            return View(phims);
        }

        public ActionResult PhimSapChieu()
        {
            var phims = repository.GetAllPhims().Where(p => p.KhoiChieu > DateTime.Now).ToList();
            return PartialView("_PhimList", phims);
        }

        public ActionResult PhimDangChieu()
        {
            var phims = repository.GetAllPhims().Where(p => p.KhoiChieu <= DateTime.Now).ToList();
            return PartialView("_PhimList", phims);
        }

        public ActionResult TatCaPhim()
        {
            var phims = repository.GetAllPhims();
            return PartialView("_PhimList", phims);
        }



        [HttpGet]
        public JsonResult GetPhimDetails(string maPhim)
        {
            var phim = repository.GetPhimDetails(maPhim);
            if (phim == null)
            {
                return Json(new { success = false, message = "Phim không tồn tại" }, JsonRequestBehavior.AllowGet);
            }

            var suatChieuList = repository.GetAllSuatChieu().Where(p => p.MaPhim == maPhim);
            if (suatChieuList == null || !suatChieuList.Any())
            {
                return Json(new { success = false, message = "Không có suất chiếu nào cho phim này" }, JsonRequestBehavior.AllowGet);
            }

            var response = new
            {
                success = true,
                data = phim,
                suatChieuList = suatChieuList.Select(s => new
                {
                    MaSuat = s.MaSuat,
                    KhungGio = s.KhungGio.ToString(@"hh\:mm"), 
                    NgayChieu = s.NgayChieu.ToString("yyyy-MM-dd") 
                }).ToList()
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOnePhim(string maPhim)
        {
            var phim = repository.GetPhimDetails(maPhim);
            if (phim == null)
            {
                return Json(new { success = false, message = "Phim không tồn tại" }, JsonRequestBehavior.AllowGet);
            }
            var response = new
            {
                success = true,
                data = phim,
            };

            return Json(response, JsonRequestBehavior.AllowGet);

        }
        }
}
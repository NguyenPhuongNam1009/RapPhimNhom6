using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom6_DoAn_.Models;

namespace Nhom6_DoAn_.Controllers
{
    public class MovieInfoController : Controller
    {
        private PhimRepository repository = new PhimRepository();

        private GheRepo seatRepo = new GheRepo();

        private DatVeRepository DatVeRepo = new DatVeRepository();

        [HttpGet]
        [Authorize]
        public ActionResult Index(string maPhim, string maSuat)
        {
            var phim = repository.GetPhimDetails(maPhim);
            if (phim == null)
            {
                
        // Gửi thông báo lỗi đến view
        ViewBag.ErrorMessage = "Phim không tồn tại.";
        return View(); // Trả về view Index với thông báo lỗi
    
            }


            var suatChieu = repository.GetSuatChieuDetails(maSuat);

            var ghe = seatRepo.GetAllGhe();

            var DatVeList = DatVeRepo.GetPhieuByMaSuat(maSuat);

            ViewBag.Phim = phim;
            ViewBag.SuatChieu = suatChieu;
            ViewBag.Ghe = ghe;
            if (maSuat != null)
            {
                ViewBag.MaSuat = maSuat;
            }
            ViewBag.DatVeList = DatVeList;

            var movieInfo = new MovieInfo() { TenPhim = phim.TenPhim, MaPhim = phim.MaPhim, AnhBia = phim.AnhBia, KhungGio = suatChieu.KhungGio.ToString(@"hh\:mm"), NgayChieu = suatChieu.NgayChieu.ToString("yyyy-MM-dd"), NgayChieuFormatted = suatChieu.NgayChieu.ToString("dd/MM/yyyy") , PhongChieu = suatChieu.MaPhong};
            ViewBag.MovieInfo = movieInfo;
            return View(ViewBag.MovieInfo);
        }

        

    }
}
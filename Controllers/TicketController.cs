using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom6_DoAn_.Models;
using Microsoft.AspNet.Identity;
using System.Configuration;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.SqlClient;

namespace Nhom6_DoAn_.Controllers
{
    public class TicketController : Controller
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private PhimRepository repository = new PhimRepository();

        private GheRepo seatRepo = new GheRepo();

        private List<string> MAVE;


        private DatVeRepository DatVeRepo = new DatVeRepository();

        private ApplicationDbContext1 db = new ApplicationDbContext1();

        // GET: Ticket
        public ActionResult TicketPayment(string maPhim, string maSuat, string[] maGhe)
        { 
            



        var phim = repository.GetPhimDetails(maPhim);
            var uniqueMaGhe = maGhe.Distinct().ToArray();
            if (phim == null)
            {

                // Gửi thông báo lỗi đến view
                ViewBag.ErrorMessage = "Phim không tồn tại.";

                ViewBag.MaVeList = MAVE;

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


            ViewBag.MaPhim = maPhim;
            ViewBag.MaSuat = maSuat;
            ViewBag.MaGhe = uniqueMaGhe;

            var movieInfo = new MovieInfo() { TenPhim = phim.TenPhim, MaPhim = phim.MaPhim, AnhBia = phim.AnhBia, KhungGio = suatChieu.KhungGio.ToString(@"hh\:mm"), NgayChieu = suatChieu.NgayChieu.ToString("yyyy-MM-dd"), NgayChieuFormatted = suatChieu.NgayChieu.ToString("dd/MM/yyyy"), PhongChieu = suatChieu.MaPhong };
            ViewBag.MovieInfo = movieInfo;
            return View(ViewBag.MovieInfo);
        }


        [HttpPost]
        public ActionResult ConfirmPayment(string maPhim, string maSuat, string[] maGhe)
        {
            var uniqueMaGhe = maGhe;
           

          
         


            foreach (var maGheItem in uniqueMaGhe)
            {
                

                var newDatVe = new DatVe
                {
                    
                    MaVe = DatVeRepo.GenerateUniqueMAVE(),
                    TrangThai = "Chưa đặt",
                    Email = User.Identity.Name, // Đảm bảo rằng Email được lấy đúng từ User.Identity
                    GiaVe = 45000,
                    MaSuat = maSuat,
                    MaGhe = maGheItem
                };


                    AddDatVe(newDatVe);
               
            }

            return RedirectToAction("PaymentSuccess");
        }



        public ActionResult PaymentSuccess()
        {
            return View();
        }

        public void AddDatVe(DatVe datVe)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO DATVE (MAVE, TRANGTHAI, GIAVE, MAGHE, MASUAT, Email) VALUES (@MaVe, @TrangThai, @GiaVe, @MaGhe, @MaSuat, @Email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MAVE", datVe.MaVe);
                    command.Parameters.AddWithValue("@TRANGTHAI", datVe.TrangThai);
                    command.Parameters.AddWithValue("@GIAVE", datVe.GiaVe);
                    command.Parameters.AddWithValue("@Email", datVe.Email);
                    command.Parameters.AddWithValue("@MAGHE", datVe.MaGhe);
                    command.Parameters.AddWithValue("@MASUAT", datVe.MaSuat);

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
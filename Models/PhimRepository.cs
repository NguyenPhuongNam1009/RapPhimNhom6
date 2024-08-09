using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Nhom6_DoAn_.Models;
using System;

namespace Nhom6_DoAn_.Models
{
    public class PhimRepository
    {
        private readonly string  connectionString;

        public PhimRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }


        public IEnumerable<Phim> SearchPhims(string query)
        {
            var phimList = new List<Phim>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Phim WHERE TenPhim LIKE @query";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@query", "%" + query + "%");
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var phim = new Phim
                            {
                                MaPhim = reader["MaPhim"].ToString(),
                                TenPhim = reader["TenPhim"].ToString(),
                                AnhBia = reader["AnhBia"].ToString(),
                                TheLoai = reader["TheLoai"].ToString(),
                                ThoiLuong = Convert.ToInt32(reader["ThoiLuong"]),
                                // Các thuộc tính khác
                            };
                            phimList.Add(phim);
                        }
                    }
                }
            }
            return phimList;
        }

        public IEnumerable<Phim> GetAllPhims()
        {
            var phims = new List<Phim>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PHIM";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var phim = new Phim
                    {
                        MaPhim = reader["MAPHIM"].ToString(),
                        TenPhim = reader["TENPHIM"].ToString(),
                        AnhBia = reader["ANHBIA"].ToString(),
                        TrailerUrl =reader["TRAILER"].ToString(),
                        DaoDien = reader["DAODIEN"].ToString(),
                        DienVien = reader["DIENVIEN"].ToString(),
                        TheLoai = reader["THELOAI"].ToString(),
                        ThoiLuong = (int)reader["THOILUONG"],
                        NgonNgu = reader["NGONNGU"].ToString(),
                        KhoiChieu = (DateTime)reader["KHOICHIEU"]
                    };
                    phims.Add(phim);
                }
            }

            return phims;
        }
        public Phim GetPhimDetails(string maPhim)
            
        {
            if (maPhim == null)
            {
                return null;
            }
            Phim phim = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PHIM WHERE MAPHIM = @MaPhim";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhim", maPhim);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    phim = new Phim
                    {
                        MaPhim = reader["MAPHIM"].ToString(),
                        TenPhim = reader["TENPHIM"].ToString(),
                        TrailerUrl = reader["TRAILER"].ToString(),
                        AnhBia = reader["ANHBIA"].ToString(),
                        TheLoai = reader["THELOAI"].ToString(),
                        ThoiLuong = Convert.ToInt32(reader["THOILUONG"]),
                        // Các thuộc tính khác nếu cần
                    };
                }
            }
            return phim;
        }

        public List<SuatChieu> GetAllSuatChieu()
        {
            List<SuatChieu> suatChieuList = new List<SuatChieu>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT SC.MASUAT, SC.NGAYCHIEU, SC.KHUNGGIO, SC.MAPHIM, SC.MAPHONG, P.TENPHIM, PC.TENPHONG 
                             FROM SUATCHIEU SC
                             JOIN PHIM P ON SC.MAPHIM = P.MAPHIM
                             JOIN PHONGCHIEU PC ON SC.MAPHONG = PC.MAPHONG";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SuatChieu suatChieu = new SuatChieu
                    {
                        MaSuat = reader["MASUAT"].ToString(),
                        NgayChieu = Convert.ToDateTime(reader["NGAYCHIEU"]),
                        KhungGio = TimeSpan.Parse(reader["KHUNGGIO"].ToString()),
                        MaPhim = reader["MAPHIM"].ToString(),
                        TenPhim = reader["TENPHIM"].ToString(),
                        MaPhong = reader["MAPHONG"].ToString(),
                        TenPhong = reader["TENPHONG"].ToString()
                    };
                    suatChieuList.Add(suatChieu);
                }
                reader.Close();
            }
            return suatChieuList;
        }

        public SuatChieu GetSuatChieuDetails(string maSuat)
        {
            SuatChieu suatChieu = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT SC.MASUAT, SC.NGAYCHIEU, SC.KHUNGGIO, SC.MAPHIM, SC.MAPHONG, P.TENPHIM, PC.TENPHONG 
                             FROM SUATCHIEU SC
                             JOIN PHIM P ON SC.MAPHIM = P.MAPHIM
                             JOIN PHONGCHIEU PC ON SC.MAPHONG = PC.MAPHONG";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     suatChieu = new SuatChieu
                    {
                        MaSuat = reader["MASUAT"].ToString(),
                        NgayChieu = Convert.ToDateTime(reader["NGAYCHIEU"]),
                        KhungGio = TimeSpan.Parse(reader["KHUNGGIO"].ToString()),
                        MaPhim = reader["MAPHIM"].ToString(),
                        TenPhim = reader["TENPHIM"].ToString(),
                        MaPhong = reader["MAPHONG"].ToString(),
                        TenPhong = reader["TENPHONG"].ToString()
                    };
                }
                reader.Close();
            }
            return suatChieu;
        }
        public Phim GetPhimById(string id)
        {
            Phim phim = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PHIM WHERE MAPHIM = @MaPhim";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhim", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    phim = new Phim
                    {
                        MaPhim = reader["MAPHIM"].ToString(),
                        TenPhim = reader["TENPHIM"].ToString(),
                        TrailerUrl =reader["TRAILER"].ToString(),
                        DaoDien = reader["DAODIEN"].ToString(),
                        DienVien = reader["DIENVIEN"].ToString(),
                        TheLoai = reader["THELOAI"].ToString(),
                        ThoiLuong = (int)reader["THOILUONG"],
                        NgonNgu = reader["NGONNGU"].ToString(),
                        KhoiChieu = (DateTime)reader["KHOICHIEU"]
                    };
                }
            }

            return phim;
        }

        public void AddPhim(Phim phim)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PHIM (MAPHIM, TENPHIM, DAODIEN, DIENVIEN, THELOAI, THOILUONG, NGONNGU, KHOICHIEU) " +
                               "VALUES (@MaPhim, @TenPhim, @DaoDien, @DienVien, @TheLoai, @ThoiLuong, @NgonNgu, @KhoiChieu)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhim", phim.MaPhim);
                command.Parameters.AddWithValue("@TenPhim", phim.TenPhim);
                command.Parameters.AddWithValue("@DaoDien", phim.DaoDien);
                command.Parameters.AddWithValue("@DienVien", phim.DienVien);
                command.Parameters.AddWithValue("@TheLoai", phim.TheLoai);
                command.Parameters.AddWithValue("@ThoiLuong", phim.ThoiLuong);
                command.Parameters.AddWithValue("@NgonNgu", phim.NgonNgu);
                command.Parameters.AddWithValue("@KhoiChieu", phim.KhoiChieu);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePhim(Phim phim)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE PHIM SET TENPHIM = @TenPhim, DAODIEN = @DaoDien, DIENVIEN = @DienVien, THELOAI = @TheLoai, " +
                               "THOILUONG = @ThoiLuong, NGONNGU = @NgonNgu, KHOICHIEU = @KhoiChieu WHERE MAPHIM = @MaPhim";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhim", phim.MaPhim);
                command.Parameters.AddWithValue("@TenPhim", phim.TenPhim);
                command.Parameters.AddWithValue("@DaoDien", phim.DaoDien);
                command.Parameters.AddWithValue("@DienVien", phim.DienVien);
                command.Parameters.AddWithValue("@TheLoai", phim.TheLoai);
                command.Parameters.AddWithValue("@ThoiLuong", phim.ThoiLuong);
                command.Parameters.AddWithValue("@NgonNgu", phim.NgonNgu);
                command.Parameters.AddWithValue("@KhoiChieu", phim.KhoiChieu);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeletePhim(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM PHIM WHERE MAPHIM = @MaPhim";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhim", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

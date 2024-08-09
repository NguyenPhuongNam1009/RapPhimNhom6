using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Nhom6_DoAn_.Models;
using System.Linq;


namespace Nhom6_DoAn_.Models
{
    public class DatVeRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      
     

        // Lấy tất cả các phiếu
        public IEnumerable<DatVe> GetAllDatVe()
        {
            var datVes = new List<DatVe>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM DATVE", connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        datVes.Add(new DatVe
                        {
                            MaVe = reader["MAVE"].ToString(),
                            TrangThai = reader["TRANGTHAI"].ToString(),
                            GiaVe = Convert.ToInt32(reader["GIAVE"]),
                            Email = reader["Email"].ToString(),
                            MaGhe = reader["MAGHE"].ToString(),
                            MaSuat = reader["MASUAT"].ToString()
                        });
                    }
                }
            }

            return datVes;
        }

        // Lọc các phiếu theo MASUAT
        public IEnumerable<DatVe> GetPhieuByMaSuat(string maSuat)
        {
            var datVes = new List<DatVe>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM DATVE WHERE MASUAT = @MaSuat", connection);
                command.Parameters.AddWithValue("@MaSuat", maSuat);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        datVes.Add(new DatVe
                        {
                            MaVe = reader["MAVE"].ToString(),
                            TrangThai = reader["TRANGTHAI"].ToString(),
                            GiaVe = Convert.ToInt32(reader["GIAVE"]),
                            Email = reader["Email"].ToString(),
                            MaGhe = reader["MAGHE"].ToString(),
                            MaSuat = reader["MASUAT"].ToString()
                        });
                    }
                }
            }

            return datVes;
        }
        public void AddDatVe(DatVe datVe)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO DatVes (MaVe, TrangThai, GiaVe, Email, MaGhe, MaSuat) VALUES (@MaVe, @TrangThai, @GiaVe, @Email, @MaGhe, @MaSuat)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaVe", datVe.MaVe);
                    command.Parameters.AddWithValue("@TrangThai", datVe.TrangThai);
                    command.Parameters.AddWithValue("@GiaVe", datVe.GiaVe);
                    command.Parameters.AddWithValue("@Email", datVe.Email);
                    command.Parameters.AddWithValue("@MaGhe", datVe.MaGhe);
                    command.Parameters.AddWithValue("@MaSuat", datVe.MaSuat);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Phương thức để tạo mã vé mới
        public string GenerateUniqueMAVE()
        {
            string mave;
            bool isUnique;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                do
                {
                    mave = "V" + new Random().Next(1, 10000).ToString("D4");
                    isUnique = CheckMAVEUniqueness(connection, mave);
                } while (!isUnique);
            }

            return mave;
        }

        private bool CheckMAVEUniqueness(SqlConnection connection, string mave)
        {
            string query = "SELECT COUNT(*) FROM DATVE WHERE MAVE = @MAVE";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(new SqlParameter("@MAVE", SqlDbType.VarChar) { Value = mave });
                int count = (int)command.ExecuteScalar();
                return count == 0;
            }
        }
    }
}

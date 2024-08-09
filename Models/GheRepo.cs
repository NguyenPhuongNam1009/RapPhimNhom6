using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Nhom6_DoAn_.Models;
using System;
using System.Linq;

namespace Nhom6_DoAn_.Models
{
   
    public class GheRepo
    {
        private string connectionString;

        public GheRepo()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Ghe> GetAllGhe()
        {
            List<Ghe> gheList = new List<Ghe>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM GHE";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ghe ghe = new Ghe
                            {
                                MaGhe = reader["MAGHE"].ToString(),
                                Day = reader["MADAY"].ToString()
                            };
                            gheList.Add(ghe);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
                Console.WriteLine(ex.Message);
            }
            gheList = gheList.OrderBy(g => g.Day).ThenBy(g => ExtractNumber(g.MaGhe)).ToList();

            return gheList;
        }
        private int ExtractNumber(string maGhe)
        {
            // Giả sử số ghế nằm ở cuối chuỗi MaGhe
            string number = new string(maGhe.Where(char.IsDigit).ToArray());
            int result;
            return int.TryParse(number, out result) ? result : 0;
        }
    }
}
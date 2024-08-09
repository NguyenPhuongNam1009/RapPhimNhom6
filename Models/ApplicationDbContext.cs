using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nhom6_DoAn_.Models
{
    public class ApplicationDbContext1 : DbContext
    {
        // Tên chuỗi kết nối trong file Web.config
        public class ApplicationUser : IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                return userIdentity;
            }

            // Thêm các thuộc tính bổ sung cho người dùng tại đây, nếu cần thiết
        }
        public ApplicationDbContext1() : base("DefaultConnection")
        {
        }

        // Định nghĩa các DbSet cho các bảng
        public DbSet<Phim> Phims { get; set; }
        public DbSet<SuatChieu> SuatChieus { get; set; }
        public DbSet<Ghe> Ghes { get; set; }
        public DbSet<DatVe> DatVes { get; set; }

        // Các bảng khác bạn có thể thêm ở đây
    }
}

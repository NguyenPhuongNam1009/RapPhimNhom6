using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using Nhom6_DoAn_.Models;

namespace Nhom6_DoAn_.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        private RAPPHIMEntities db = new RAPPHIMEntities();

        // GET: Admin/Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHashed = HashPassword(model.Password);

                // Sử dụng AsEnumerable() để thực hiện truy vấn trong bộ nhớ (RAM)
                var adminUser = db.Tbl_Users
                    .AsEnumerable() // Chuyển đổi sang IEnumerable để thực hiện lọc trong bộ nhớ
                    .FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == passwordHashed && u.Role == "admin");

                if (adminUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    Session["User"] = adminUser;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }



        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Đăng xuất
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            return RedirectToAction("Login");
        }

    }
}
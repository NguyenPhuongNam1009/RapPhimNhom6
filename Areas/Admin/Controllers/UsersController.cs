using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom6_DoAn_;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;


namespace Nhom6_DoAn_.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private RAPPHIMEntities db = new RAPPHIMEntities();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Tbl_Users.ToList();
            ViewBag.Roles = new SelectList(db.Tbl_IndentityRoles, "Name", "Name");
            return View(users);
        }


        public ActionResult AddRole(string id)
        {
            var user = db.Tbl_Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Tạo danh sách vai trò với một giá trị cứng
            var roles = new List<SelectListItem>
    {
        new SelectListItem { Value = "admin", Text = "Admin" },
        new SelectListItem { Value = "user", Text = "User" }
    };
            ViewBag.Roles = new SelectList(roles, "Value", "Text");

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRole(string userId, string selectedRole)
        {
            // Kiểm tra nếu userId và selectedRole không rỗng
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(selectedRole))
            {
                ModelState.AddModelError("", "Invalid role or user ID.");
                return RedirectToAction("Index");
            }

            // Tìm người dùng theo userId
            var user = db.Tbl_Users.Find(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return RedirectToAction("Index");
            }

            // Tìm vai trò theo tên vai trò
            var role = db.Tbl_IndentityRoles
                .FirstOrDefault(r => r.Name == selectedRole);

            if (role == null)
            {
                ModelState.AddModelError("", "Invalid role selected.");
                return RedirectToAction("Index");
            }

            // Kiểm tra xem người dùng đã có vai trò này chưa
            if (user.Tbl_IndentityRoles.Contains(role))
            {
                ModelState.AddModelError("", "User already has this role.");
                return RedirectToAction("Index");
            }

            // Thêm vai trò vào người dùng
            user.Tbl_IndentityRoles.Add(role);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving changes: " + ex.Message);
            }

            return RedirectToAction("Index");
        }


        public ActionResult SeedRoles()
        {
            var roles = new List<Tbl_IndentityRoles>
    {
        new Tbl_IndentityRoles { Id = "admin", Name = "Admin" },
        new Tbl_IndentityRoles { Id = "user", Name = "User" }
    };

            foreach (var role in roles)
            {
                if (!db.Tbl_IndentityRoles.Any(r => r.Id == role.Id))
                {
                    db.Tbl_IndentityRoles.Add(role);
                }
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Users tbl_Users = await db.Tbl_Users.FindAsync(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,Password,FullName,PhoneNumber")] Tbl_Users tbl_Users)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Hash mật khẩu trước khi lưu vào cơ sở dữ liệu
                    tbl_Users.PasswordHash = HashPassword(tbl_Users.Password);

                    db.Tbl_Users.Add(tbl_Users);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log exception details here
                    ModelState.AddModelError("", "An error occurred while saving the data. Please try again.");
                }
            }

            return View(tbl_Users);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // GET: Admin/Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Users tbl_Users = await db.Tbl_Users.FindAsync(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,PhoneNumber,UserName,FullName")] Tbl_Users tbl_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tbl_Users);
        }

        // GET: Admin/Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Users tbl_Users = await db.Tbl_Users.FindAsync(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Tbl_Users tbl_Users = await db.Tbl_Users.FindAsync(id);
            db.Tbl_Users.Remove(tbl_Users);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

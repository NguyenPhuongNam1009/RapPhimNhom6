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
using System.Data.Entity.Validation;
using System.IO; // Thư viện cung cấp lớp Path



namespace Nhom6_DoAn_.Areas.Admin.Controllers
{
    
    public class PhimController : Controller
    {
        private RAPPHIMEntities db = new RAPPHIMEntities();

        // GET: Admin/Phim
        public async Task<ActionResult> Index()
        {
            return View(await db.PHIMs.ToListAsync());
        }

        // GET: Admin/Phim/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIM pHIM = await db.PHIMs.FindAsync(id);
            if (pHIM == null)
            {
                return HttpNotFound();
            }
            return View(pHIM);
        }

        // GET: Admin/Phim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Phim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PHIM pHIM, HttpPostedFileBase PosterFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (PosterFile != null && PosterFile.ContentLength > 0)
                    {
                        // Tạo đường dẫn file
                        var fileName = Path.GetFileName(PosterFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/Poster"), fileName);

                        // Lưu file vào thư mục gốc
                        PosterFile.SaveAs(path);
                    }

                        db.PHIMs.Add(pHIM); 
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
            return View(pHIM);
        }


        // GET: Admin/Phim/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIM pHIM = await db.PHIMs.FindAsync(id);
            if (pHIM == null)
            {
                return HttpNotFound();
            }
            return View(pHIM);
        }

        // POST: Admin/Phim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MAPHIM,TENPHIM,ANHBIA,TRAILER,DAODIEN,DIENVIEN,THELOAI,THOILUONG,NGONNGU,KHOICHIEU")] PHIM pHIM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pHIM);
        }

        // GET: Admin/Phim/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIM pHIM = await db.PHIMs.FindAsync(id);
            if (pHIM == null)
            {
                return HttpNotFound();
            }
            return View(pHIM);
        }

        // POST: Admin/Phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PHIM pHIM = await db.PHIMs.FindAsync(id);
            db.PHIMs.Remove(pHIM);
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

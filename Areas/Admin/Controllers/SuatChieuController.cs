using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using Nhom6_DoAn_;

namespace Nhom6_DoAn_.Areas.Admin.Controllers
{
    public class SuatChieuController : Controller
    {
        private RAPPHIMEntities db = new RAPPHIMEntities();

        // GET: Admin/SuatChieu
        public async Task<ActionResult> Index()
        {
            var sUATCHIEUx = db.SUATCHIEUx.Include(s => s.PHIM).Include(s => s.PHONGCHIEU);
            return View(await sUATCHIEUx.ToListAsync());
        }

        // GET: Admin/SuatChieu/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUATCHIEU sUATCHIEU = await db.SUATCHIEUx.FindAsync(id);
            if (sUATCHIEU == null)
            {
                return HttpNotFound();
            }
            return View(sUATCHIEU);
        }

        // GET: Admin/SuatChieu/Create
        public ActionResult Create()
        {
            ViewBag.MAPHIM = new SelectList(db.PHIMs, "MAPHIM", "TENPHIM");
            ViewBag.MAPHONG = new SelectList(db.PHONGCHIEUx, "MAPHONG", "TENPHONG");
            return View();
        }

        // POST: Admin/SuatChieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MASUAT,NGAYCHIEU,KHUNGGIO,MAPHIM,MAPHONG")] SUATCHIEU sUATCHIEU)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SUATCHIEUx.Add(sUATCHIEU);
                    await db.SaveChangesAsync();
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

            ViewBag.MAPHIM = new SelectList(db.PHIMs, "MAPHIM", "TENPHIM", sUATCHIEU.MAPHIM);
            ViewBag.MAPHONG = new SelectList(db.PHONGCHIEUx, "MAPHONG", "TENPHONG", sUATCHIEU.MAPHONG);
            return View(sUATCHIEU);
        }

        // GET: Admin/SuatChieu/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUATCHIEU sUATCHIEU = await db.SUATCHIEUx.FindAsync(id);
            if (sUATCHIEU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPHIM = new SelectList(db.PHIMs, "MAPHIM", "TENPHIM", sUATCHIEU.MAPHIM);
            ViewBag.MAPHONG = new SelectList(db.PHONGCHIEUx, "MAPHONG", "TENPHONG", sUATCHIEU.MAPHONG);
            return View(sUATCHIEU);
        }

        // POST: Admin/SuatChieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MASUAT,NGAYCHIEU,KHUNGGIO,MAPHIM,MAPHONG")] SUATCHIEU sUATCHIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUATCHIEU).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MAPHIM = new SelectList(db.PHIMs, "MAPHIM", "TENPHIM", sUATCHIEU.MAPHIM);
            ViewBag.MAPHONG = new SelectList(db.PHONGCHIEUx, "MAPHONG", "TENPHONG", sUATCHIEU.MAPHONG);
            return View(sUATCHIEU);
        }

        // GET: Admin/SuatChieu/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUATCHIEU sUATCHIEU = await db.SUATCHIEUx.FindAsync(id);
            if (sUATCHIEU == null)
            {
                return HttpNotFound();
            }
            return View(sUATCHIEU);
        }

        // POST: Admin/SuatChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            SUATCHIEU sUATCHIEU = await db.SUATCHIEUx.FindAsync(id);
            db.SUATCHIEUx.Remove(sUATCHIEU);
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

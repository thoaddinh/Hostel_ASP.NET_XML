using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VectorHostel.Models;

namespace VectorHostel.Controllers
{
    public class chi_tiet_dat_giuongController : Controller
    {
        private VectorHostelEntities1 db = new VectorHostelEntities1();
        
        public ActionResult thanhtoan(String makh)
        {

            ViewBag.makh = makh;
            return View();
        }
        // GET: chi_tiet_dat_giuong
        public async Task<ActionResult> Index()
        {
            var chi_tiet_dat_giuong = db.chi_tiet_dat_giuong.Include(c => c.giuong).Include(c => c.PHIEUDATGIUONG);
            return View(await chi_tiet_dat_giuong.ToListAsync());
        }

        // GET: chi_tiet_dat_giuong/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chi_tiet_dat_giuong chi_tiet_dat_giuong = await db.chi_tiet_dat_giuong.FindAsync(id);
            if (chi_tiet_dat_giuong == null)
            {
                return HttpNotFound();
            }
            return View(chi_tiet_dat_giuong);
        }

        // GET: chi_tiet_dat_giuong/Create
        public ActionResult Create()
        {
            ViewBag.magiuong = new SelectList(db.giuong, "magiuong", "maphong");
            ViewBag.maphieu = new SelectList(db.PHIEUDATGIUONG, "maphieu", "makh");
            return View();
        }

        // POST: chi_tiet_dat_giuong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "maphieu,magiuong,ngayden,ngaydi")] chi_tiet_dat_giuong chi_tiet_dat_giuong)
        {
            if (ModelState.IsValid)
            {
                db.chi_tiet_dat_giuong.Add(chi_tiet_dat_giuong);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.magiuong = new SelectList(db.giuong, "magiuong", "maphong", chi_tiet_dat_giuong.magiuong);
            ViewBag.maphieu = new SelectList(db.PHIEUDATGIUONG, "maphieu", "makh", chi_tiet_dat_giuong.maphieu);
            return View(chi_tiet_dat_giuong);
        }

        // GET: chi_tiet_dat_giuong/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chi_tiet_dat_giuong chi_tiet_dat_giuong = await db.chi_tiet_dat_giuong.FindAsync(id);
            if (chi_tiet_dat_giuong == null)
            {
                return HttpNotFound();
            }
            ViewBag.magiuong = new SelectList(db.giuong, "magiuong", "maphong", chi_tiet_dat_giuong.magiuong);
            ViewBag.maphieu = new SelectList(db.PHIEUDATGIUONG, "maphieu", "makh", chi_tiet_dat_giuong.maphieu);
            return View(chi_tiet_dat_giuong);
        }

        // POST: chi_tiet_dat_giuong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "maphieu,magiuong,ngayden,ngaydi")] chi_tiet_dat_giuong chi_tiet_dat_giuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chi_tiet_dat_giuong).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.magiuong = new SelectList(db.giuong, "magiuong", "maphong", chi_tiet_dat_giuong.magiuong);
            ViewBag.maphieu = new SelectList(db.PHIEUDATGIUONG, "maphieu", "makh", chi_tiet_dat_giuong.maphieu);
            return View(chi_tiet_dat_giuong);
        }

        // GET: chi_tiet_dat_giuong/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chi_tiet_dat_giuong chi_tiet_dat_giuong = await db.chi_tiet_dat_giuong.FindAsync(id);
            if (chi_tiet_dat_giuong == null)
            {
                return HttpNotFound();
            }
            return View(chi_tiet_dat_giuong);
        }

        // POST: chi_tiet_dat_giuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            chi_tiet_dat_giuong chi_tiet_dat_giuong = await db.chi_tiet_dat_giuong.FindAsync(id);
            db.chi_tiet_dat_giuong.Remove(chi_tiet_dat_giuong);
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

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
    public class loaigiuongsController : Controller
    {
        private VectorHostelEntities1 db = new VectorHostelEntities1();

        // GET: loaigiuongs
        public async Task<ActionResult> Index()
        {
            return View(await db.loaigiuong.ToListAsync());
        }

        // GET: loaigiuongs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaigiuong loaigiuong = await db.loaigiuong.FindAsync(id);
            if (loaigiuong == null)
            {
                return HttpNotFound();
            }
            return View(loaigiuong);
        }

        // GET: loaigiuongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: loaigiuongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "malg,tenlg,mota,dongia")] loaigiuong loaigiuong)
        {
            if (ModelState.IsValid)
            {
                db.loaigiuong.Add(loaigiuong);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loaigiuong);
        }

        // GET: loaigiuongs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaigiuong loaigiuong = await db.loaigiuong.FindAsync(id);
            if (loaigiuong == null)
            {
                return HttpNotFound();
            }
            return View(loaigiuong);
        }

        // POST: loaigiuongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "malg,tenlg,mota,dongia")] loaigiuong loaigiuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaigiuong).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loaigiuong);
        }

        // GET: loaigiuongs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaigiuong loaigiuong = await db.loaigiuong.FindAsync(id);
            if (loaigiuong == null)
            {
                return HttpNotFound();
            }
            return View(loaigiuong);
        }

        // POST: loaigiuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            loaigiuong loaigiuong = await db.loaigiuong.FindAsync(id);
            db.loaigiuong.Remove(loaigiuong);
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

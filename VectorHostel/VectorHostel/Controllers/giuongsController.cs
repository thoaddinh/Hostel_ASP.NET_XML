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
    public class giuongsController : Controller
    {
        private VectorHostelEntities1 db = new VectorHostelEntities1();
        public String LayMaGiuong()
        {
            var mamax = db.giuong.ToList().Select(n => n.magiuong).Max();
            int maGiuong = int.Parse(mamax.Substring(1).ToString()) + 1;
            String giuong = String.Concat("00", maGiuong.ToString());
            return "G" + giuong.Substring(maGiuong.ToString().Length - 1);
        }
        // GET: giuongs
        public async Task<ActionResult> Index()
        {
            var giuong = db.giuong.Include(g => g.loaigiuong).Include(g => g.phong);
            
            return View(await giuong.ToListAsync());
        }

        // GET: giuongs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giuong giuong = await db.giuong.FindAsync(id);
            if (giuong == null)
            {
                return HttpNotFound();
            }
            return View(giuong);
        }

        // GET: giuongs/Create
        public ActionResult Create()
        {
            ViewBag.mg = LayMaGiuong();
            ViewBag.malg = new SelectList(db.loaigiuong, "malg", "tenlg");
            ViewBag.maphong = new SelectList(db.phong, "maphong", "tenphong");
            return View();
        }

        // POST: giuongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "magiuong,maphong,mota,malg,tinhtrang,hinhanh")] giuong giuong)
        {
            
          
            if (ModelState.IsValid)
            {
                giuong.magiuong = LayMaGiuong();
                giuong.tinhtrang = false;
                
                db.giuong.Add(giuong);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.malg = new SelectList(db.loaigiuong, "malg", "tenlg", giuong.malg);
            ViewBag.maphong = new SelectList(db.phong, "maphong", "tenphong", giuong.maphong);
            return View(giuong);
        }

        // GET: giuongs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giuong giuong = await db.giuong.FindAsync(id);
            if (giuong == null)
            {
                
                return HttpNotFound();
            }
            ViewBag.malg = new SelectList(db.loaigiuong, "malg", "tenlg", giuong.malg);
            ViewBag.maphong = new SelectList(db.phong, "maphong", "tenphong", giuong.maphong);
            return View(giuong);
        }

        // POST: giuongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "magiuong,maphong,mota,malg,tinhtrang,hinhanh")] giuong giuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giuong).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.malg = new SelectList(db.loaigiuong, "malg", "tenlg", giuong.malg);
            ViewBag.maphong = new SelectList(db.phong, "maphong", "tenphong", giuong.maphong);
            return View(giuong);
        }

        // GET: giuongs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giuong giuong = await db.giuong.FindAsync(id);
            if (giuong == null)
            {
                return HttpNotFound();
            }
            return View(giuong);
        }

        // POST: giuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            giuong giuong = await db.giuong.FindAsync(id);
            db.giuong.Remove(giuong);
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

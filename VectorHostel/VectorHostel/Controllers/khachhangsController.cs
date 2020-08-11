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
    public class khachhangsController : Controller
    {
        private VectorHostelEntities1 db = new VectorHostelEntities1();

        public ActionResult TimKiemNC(string makh="",string hoten="",string ngaysinh="", string sdt="",string diachi="",string gioitinh="",bool loaitimkiem = false )
        {
            
            if (gioitinh != "1" && gioitinh != "0") gioitinh = null;
            ViewBag.makh = makh;
            ViewBag.hoten = hoten;
            ViewBag.ngaysinh = ngaysinh;
            ViewBag.gioitinh = gioitinh;
            ViewBag.sdt = sdt;
            ViewBag.diachi = diachi;
            
            if (loaitimkiem)
            {
                var kh = db.khachhang.SqlQuery("khachhang_timkiem'" + makh + "','" + hoten + "','" + ngaysinh + "','" + gioitinh + "','" + diachi + "','" + sdt + "'");
                return View(kh.ToList());
            }
            else
            {
                var kh = db.khachhang.SqlQuery("khachhang_timkiem_khongCX'" + makh + "','" + hoten + "','" + ngaysinh + "','" + gioitinh + "','" + diachi + "','" + sdt + "'");
                return View(kh.ToList());
            }
           

        }
        // GET: khachhangs
        public async Task<ActionResult> Index()
        {
            return View(await db.khachhang.ToListAsync());
        }

        // GET: khachhangs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = await db.khachhang.FindAsync(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // GET: khachhangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: khachhangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "makh,hoten,ngaysinh,sdt,diachi,gioitinh")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.khachhang.Add(khachhang);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(khachhang);
        }

        // GET: khachhangs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = await db.khachhang.FindAsync(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: khachhangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "makh,hoten,ngaysinh,sdt,diachi,gioitinh")] khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }

        // GET: khachhangs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            khachhang khachhang = await db.khachhang.FindAsync(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            khachhang khachhang = await db.khachhang.FindAsync(id);
            db.khachhang.Remove(khachhang);
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

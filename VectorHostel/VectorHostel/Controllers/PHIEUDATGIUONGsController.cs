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
    public class PHIEUDATGIUONGsController : Controller
    {
        private VectorHostelEntities1 db = new VectorHostelEntities1();
        public String LayMaKH()
        {
            var mamax = db.khachhang.ToList().Select(n => n.makh).Max();
            if (mamax == null) return "KH001";
            int maKH = int.Parse(mamax.Substring(2).ToString()) + 1;
            String kh = String.Concat("000", maKH.ToString());
            return "KH" + kh.Substring(maKH.ToString().Length - 1);
        }
        public String LayMaPhieu()
        {
            var mamax = db.PHIEUDATGIUONG.ToList().Select(n => n.maphieu).Max();
            if (mamax == null) return "P001";
            int maPhieu = int.Parse(mamax.Substring(2).ToString()) + 1;
            String p = String.Concat("000", maPhieu.ToString());
            return "P" + p.Substring(maPhieu.ToString().Length - 1);
        }
        // GET: PHIEUDATGIUONGs
        public async Task<ActionResult> Index()
        {
            var pHIEUDATGIUONG = db.PHIEUDATGIUONG.Include(p => p.khachhang).Include(p=>p.chi_tiet_dat_giuong);
            return View(await pHIEUDATGIUONG.ToListAsync());
        }

        // GET: PHIEUDATGIUONGs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDATGIUONG pHIEUDATGIUONG = await db.PHIEUDATGIUONG.FindAsync(id);
            if (pHIEUDATGIUONG == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUDATGIUONG);
        }
       
        // GET: PHIEUDATGIUONGs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.maloaigiuong = new SelectList(db.loaigiuong, "malg", "tenlg");
        //    ViewBag.giuong = new SelectList(db.giuong.Where(g=>g.tinhtrang==false), "magiuong","magiuong");
        //    return View();
        //}
        public ActionResult Create(String malg)
        {
            if (malg == null)
            {
                ViewBag.maloaigiuong = new SelectList(db.loaigiuong, "malg", "tenlg");
                ViewBag.giuong = new SelectList(db.giuong.Where(g => g.tinhtrang == false), "magiuong", "magiuong");
            }

            else
            {
                ViewBag.maloaigiuong = new SelectList(db.loaigiuong.Where(g => g.malg == malg), "malg", "tenlg");

                ViewBag.giuong = new SelectList(db.giuong.Where(g => g.tinhtrang == false && g.malg == malg), "magiuong", "magiuong");
            }
            ViewBag.makh = LayMaKH();
            ViewBag.maphieu = LayMaPhieu();
            return View();
        }
        // POST: PHIEUDATGIUONGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Exclude ="makh,maphieu")] PHIEUDATGIUONG pHIEUDATGIUONG)
        {
         
            if (ModelState.IsValid)
            {
                
                khachhang kh = new khachhang();
                kh.makh = LayMaKH();
                pHIEUDATGIUONG.maphieu = LayMaPhieu();
                chi_tiet_dat_giuong ct = new chi_tiet_dat_giuong();
                ct.maphieu = LayMaPhieu();

                kh.hoten = pHIEUDATGIUONG.khachhang.hoten;
                kh.gioitinh = pHIEUDATGIUONG.khachhang.gioitinh;
                kh.ngaysinh = pHIEUDATGIUONG.khachhang.ngaysinh;
                kh.sdt = pHIEUDATGIUONG.khachhang.sdt;
                kh.diachi = pHIEUDATGIUONG.khachhang.diachi;
                db.khachhang.Add(kh);

                ct.ngayden = pHIEUDATGIUONG.chi_tiet_dat_giuong.ToList().FirstOrDefault().ngayden;
                ct.ngaydi = pHIEUDATGIUONG.chi_tiet_dat_giuong.ToList().FirstOrDefault().ngaydi;
                
                ViewBag.giuong = Request.Form["giuong"];
                ct.magiuong = ViewBag.giuong;
                giuong g = await db.giuong.FindAsync(ViewBag.giuong);
                g.tinhtrang = true;
                pHIEUDATGIUONG.makh = LayMaKH();
                db.PHIEUDATGIUONG.Add(pHIEUDATGIUONG);
                db.chi_tiet_dat_giuong.Add(ct);
               await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pHIEUDATGIUONG);
        }

        // GET: PHIEUDATGIUONGs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDATGIUONG pHIEUDATGIUONG = await db.PHIEUDATGIUONG.FindAsync(id);
            if (pHIEUDATGIUONG == null)
            {
                return HttpNotFound();
            }
            ViewBag.makh = new SelectList(db.khachhang, "makh", "hoten", pHIEUDATGIUONG.makh);
            return View(pHIEUDATGIUONG);
        }

        // POST: PHIEUDATGIUONGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "maphieu,makh,ngayden,ngaydi,soluong_giuong,magiuong")] PHIEUDATGIUONG pHIEUDATGIUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUDATGIUONG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.makh = new SelectList(db.khachhang, "makh", "hoten", pHIEUDATGIUONG.makh);
            return View(pHIEUDATGIUONG);
        }

        // GET: PHIEUDATGIUONGs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDATGIUONG pHIEUDATGIUONG = await db.PHIEUDATGIUONG.FindAsync(id);
            if (pHIEUDATGIUONG == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUDATGIUONG);
        }

        // POST: PHIEUDATGIUONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PHIEUDATGIUONG pHIEUDATGIUONG = await db.PHIEUDATGIUONG.FindAsync(id);
            db.PHIEUDATGIUONG.Remove(pHIEUDATGIUONG);
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

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
    public class HomeController : Controller
    {
        // GET: Home
        private VectorHostelEntities1 db = new VectorHostelEntities1();
        public ActionResult Home()
        {
            var loaigiuong =  db.loaigiuong.ToList();

            return View(loaigiuong);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HHHKN.Models;
namespace HHHKN.Controllers
{
    public class TrangChuController : Controller
    {
        private HHHKNDBContext db = new HHHKNDBContext();
        // GET: TrangChu
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
    }
}
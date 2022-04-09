using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HHHKN.Library;
using HHHKN.Models;
using System.IO;

namespace HHHKN.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private HHHKNDBContext db = new HHHKNDBContext();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var list = db.Products
                .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductCategory
                    {
                        Id = p.Id,
                        CatId = p.CatId,
                        Name = p.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Metadesc = p.Metadesc,
                        Metakey = p.Metakey,
                        Img = p.Img,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        Created_At = p.Created_At,
                        Created_By = p.Created_By,
                        Update_At = p.Update_At,
                        Updated_By = p.Updated_By,
                        Status = p.Status,
                        CatName = c.Name
                    }
                  )

                .Where(m => m.Status != 0)
                .OrderByDescending(m => m.Created_At)
                .ToList();
            return View(list);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(product.Name);
                product.Slug = slug;
                product.Created_At = DateTime.Now;
                product.Created_By = int.Parse(Session["UserAdmin"].ToString());
                product.Update_At = DateTime.Now;
                product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
                //HÌNH ẢNH
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".png", ".jpeg", ".gif" };
                if (Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        //Upload hình
                        string imgName = slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        product.Img = imgName; //LƯU VÀO CSDL
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Product/"), imgName);
                        Img.SaveAs(PathImg); //LƯU FILE LÊN SERVER
                    }
                }

                //
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(product.Name);
                product.Slug = slug;
                product.Update_At = DateTime.Now;
                product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
                //HÌNH ẢNH
                var Img = Request.Files["fileimg"];
                string[] FileExtention = { ".jpg", ".png", "jpeg", "gif" };
                if (Img.ContentLength != 0)
                {
                    if (FileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        if (product.Img == null)
                        {
                            //Upload hình
                            string imgName1 = slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                            product.Img = imgName1;// LƯU VÀO CSDL
                            string PathImg1 = Path.Combine(Server.MapPath("~/Public/images/Product/"), imgName1);
                            Img.SaveAs(PathImg1); //LƯU FILE LÊN SERVER
                        }
                        //XOÁ HÌNH
                        String DelPath = Path.Combine(Server.MapPath("~/Public/images/Product/"), product.Img);
                        if (System.IO.File.Exists(DelPath))
                        {
                            System.IO.File.Delete(DelPath);
                        }
                        //Upload hình
                        string imgName = slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                        product.Img = imgName;// LƯU VÀO CSDL
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Product/"), imgName);
                        Img.SaveAs(PathImg); //LƯU FILE LÊN SERVER
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Product product = db.Products.Find(id);
            int status = (product.Status == 1) ? 2 : 1;
            product.Status = status;
            product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
            product.Update_At = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //XOÁ VÀO THÙNG RÁC
        public ActionResult DelTrash(int id)
        {

            Product product = db.Products.Find(id);
            product.Status = 0;
            product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
            product.Update_At = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        //KHÔI PHỤC MẪU TIN STATUS = 2
        public ActionResult ReTrash(int id)
        {

            Product product = db.Products.Find(id);
            product.Status = 2;
            product.Updated_By = int.Parse(Session["UserAdmin"].ToString());
            product.Update_At = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Trash", "Product");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalYearProject.Models;

namespace FinalYearProject.Controllers
{
    public class ProductsController : Controller
    {
        private ServicesDataEntities db = new ServicesDataEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Advertiser);
            return View(products.ToList());
        }

        public ActionResult products_by_name(int? id)
        {

            if (id == null)
            {
                id = 1;

            }

            var total_record = db.Products.Count();
            var total_pages = total_record / 15;

            var page_id = (int)id;
            if (page_id > total_pages)
            {
                return HttpNotFound();
            }
            var products=db.Products.Include(a=>a.Advertiser).OrderBy(a=>a.Name).Skip((page_id - 1) * 15).Take(15);

            

            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;




            return View(products.ToList());
        }


        public ActionResult products_by_rank(int? id)
        {

            if (id == null)
            {
                id = 1;

            }

            var total_record = db.Products.Count();
            var total_pages = total_record / 15;

            var page_id = (int)id;
            if (page_id > total_pages)
            {
                return HttpNotFound();
            }

            var products = db.Products.Include(a => a.Advertiser).Where(a=>!(a.Advertiser.NetworkRank.Equals("new"))).OrderByDescending(a => a.Advertiser.NetworkRank).Skip((page_id - 1) * 15).Take(15);



            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(products.ToList());
        }


        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdID,AdvertiserID,AdvertisorCategory,BuyUrl,CatalogID,Currency,Description,ImageUrl,InStock,ManufacturerSKU,Name,Price,RetailPrice,SalePrice,SKU,UPC,ISBN,ProductAutoKey,CreateDate,ModifyDate,ManufactureName")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", product.AdvertiserID);
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", product.AdvertiserID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdID,AdvertiserID,AdvertisorCategory,BuyUrl,CatalogID,Currency,Description,ImageUrl,InStock,ManufacturerSKU,Name,Price,RetailPrice,SalePrice,SKU,UPC,ISBN,ProductAutoKey,CreateDate,ModifyDate,ManufactureName")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", product.AdvertiserID);
            return View(product);
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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

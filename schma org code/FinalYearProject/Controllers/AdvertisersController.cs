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
    public class AdvertisersController : Controller
    {
        private ServicesDataEntities db = new ServicesDataEntities();

        // GET: Advertisers
        public ActionResult Index()
        {
            var advertisers = (db.Advertisers.Include(a => a.Category).Include(a => a.Category1).Include(a => a.Language)).Take(5);
            return View(advertisers.ToList());
        }


        public ActionResult advertisers_by_name(int? id)
        {
          
            if (id == null)
            {
                id = 1;
          
            }
            
            var total_record = db.Advertisers.Count();
            var total_pages = total_record / 20;

            var page_id = (int) id;

            if (page_id > total_pages)
            {
                return HttpNotFound();
            }


                var advertisers = (db.Advertisers).Include(a => a.Category1).OrderBy(a => a.Name).Skip((page_id-1)*20).Take(20);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;
            
            return View(advertisers.ToList());
        }


        public ActionResult advertisers_by_rank(int? id)
        {

            if (id == null)
            {
                id = 1;

            }

            var total_record = db.Advertisers.Count();
            var total_pages = total_record / 20;

            var page_id = (int)id;

            if (page_id > total_pages)
            {
                return HttpNotFound();
            }


            var advertisers = (db.Advertisers).Include(a => a.Category1).Where(a=>!a.NetworkRank.Equals("new")).OrderByDescending(a => a.NetworkRank).Skip((page_id - 1) * 20).Take(20);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(advertisers.ToList());
        }




        // GET: Advertisers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertiser advertiser = db.Advertisers.Find(id);
            if (advertiser == null)
            {
                return HttpNotFound();
            }
            return View(advertiser);
        }

        // GET: Advertisers/Create
        public ActionResult Create()
        {
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Category1");
            ViewBag.ChildCategoryID = new SelectList(db.Categories, "CategoryID", "Category1");
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages");
            return View();
        }

        // POST: Advertisers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvertiserID,AccountStatus,SevenDayEpc,ThreeMonthEpc,LanguageID,Name,Url,RelationshipStatus,MobileTracking,NetworkRank,ParentCategoryID,ChildCategoryID,PerformanceIncentive,CreateDate,ModifyDate")] Advertiser advertiser)
        {
            if (ModelState.IsValid)
            {
                db.Advertisers.Add(advertiser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ParentCategoryID);
            ViewBag.ChildCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ChildCategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", advertiser.LanguageID);
            return View(advertiser);
        }

        // GET: Advertisers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertiser advertiser = db.Advertisers.Find(id);
            if (advertiser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ParentCategoryID);
            ViewBag.ChildCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ChildCategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", advertiser.LanguageID);
            return View(advertiser);
        }

        // POST: Advertisers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvertiserID,AccountStatus,SevenDayEpc,ThreeMonthEpc,LanguageID,Name,Url,RelationshipStatus,MobileTracking,NetworkRank,ParentCategoryID,ChildCategoryID,PerformanceIncentive,CreateDate,ModifyDate")] Advertiser advertiser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertiser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ParentCategoryID);
            ViewBag.ChildCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ChildCategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", advertiser.LanguageID);
            return View(advertiser);

            
        }

        // GET: Advertisers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertiser advertiser = db.Advertisers.Find(id);
            if (advertiser == null)
            {
                return HttpNotFound();
            }
            return View(advertiser);
        }

        // POST: Advertisers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertiser advertiser = db.Advertisers.Find(id);
            db.Advertisers.Remove(advertiser);
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

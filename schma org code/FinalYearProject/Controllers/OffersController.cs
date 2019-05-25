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
    public class OffersController : Controller
    {
        private ServicesDataEntities db = new ServicesDataEntities();

        // GET: Offers


            public ActionResult Newest_Offers (int? id)
        {
            if (id == null)
            {
                id = 1;

            }



            var total_record = db.OfferApproveds.Count();
            var total_pages = total_record / 5;

            var page_id = (int)id;

            if (page_id > total_pages)
            {
                return HttpNotFound();
            }

            var offers=db.Offers.Where(a => a.OfferApproved.OfferID == a.OfferID).Include(a=>a.OfferApproved).Include(a=>a.Category).Include(a=>a.Advertiser).Include(a=>a.Promotion).OrderBy(a=>a.CreateDate).Skip((page_id - 1) * 5).Take(5);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(offers);
        }


        public ActionResult Top_Offers(int? id)
        {
            if (id == null)
            {
                id = 1;

            }



            var total_record = db.OfferApproveds.Count();
            var total_pages = total_record / 5;

            var page_id = (int)id;

            if (page_id > total_pages)
            {
                return HttpNotFound();
            }

            //var offers = db.Offers.Include(a => a.Category).Include(a => a.Advertiser).Include(a => a.Promotion).Where(a=>!(a.Advertiser.NetworkRank.Equals("new"))).OrderBy(a=>a.Advertiser.NetworkRank).OrderBy(a => a.CreateDate).Skip((page_id - 1) * 5).Take(5);
            var offers = db.Offers.Where(a => a.OfferApproved.OfferID == a.OfferID).Include(a => a.OfferApproved).Include(a => a.Category).Include(a => a.Advertiser).Include(a => a.Promotion).OrderBy(a => a.CreateDate).Skip((page_id - 1) * 5).Take(5);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(offers);
        }








        public ActionResult Index()
        {
            var offers = db.Offers.Include(o => o.Advertiser).Include(o => o.Category).Include(o => o.Language).Include(o => o.LinkType).Include(o => o.Promotion);
            return View(offers.ToList());
        }

        // GET: Offers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // GET: Offers/Create
        public ActionResult Create()
        {
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Category1");
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages");
            ViewBag.LinkTypeID = new SelectList(db.LinkTypes, "LinkTypeID", "LinkType1");
            ViewBag.PromotionID = new SelectList(db.Promotions, "PromotionID", "PromotionType");
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvertiserID,CategoryID,ClickCommission,LanguageID,LeadCommission,Description,Destination,LinkID,LinkTypeID,PerformanceIncentive,PromotionEndDate,PromotionStartDate,PromotionID,CouponCode,RelationStatus,SalesCommission,SevenDayEPC,ThreeMonthEPC,ClickUrl,LinkName,OfferID,CreateDate,modfiyDate")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", offer.AdvertiserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Category1", offer.CategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", offer.LanguageID);
            ViewBag.LinkTypeID = new SelectList(db.LinkTypes, "LinkTypeID", "LinkType1", offer.LinkTypeID);
            ViewBag.PromotionID = new SelectList(db.Promotions, "PromotionID", "PromotionType", offer.PromotionID);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", offer.AdvertiserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Category1", offer.CategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", offer.LanguageID);
            ViewBag.LinkTypeID = new SelectList(db.LinkTypes, "LinkTypeID", "LinkType1", offer.LinkTypeID);
            ViewBag.PromotionID = new SelectList(db.Promotions, "PromotionID", "PromotionType", offer.PromotionID);
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvertiserID,CategoryID,ClickCommission,LanguageID,LeadCommission,Description,Destination,LinkID,LinkTypeID,PerformanceIncentive,PromotionEndDate,PromotionStartDate,PromotionID,CouponCode,RelationStatus,SalesCommission,SevenDayEPC,ThreeMonthEPC,ClickUrl,LinkName,OfferID,CreateDate,modfiyDate")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", offer.AdvertiserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Category1", offer.CategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", offer.LanguageID);
            ViewBag.LinkTypeID = new SelectList(db.LinkTypes, "LinkTypeID", "LinkType1", offer.LinkTypeID);
            ViewBag.PromotionID = new SelectList(db.Promotions, "PromotionID", "PromotionType", offer.PromotionID);
            return View(offer);
        }

        // GET: Offers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
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

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HtmlAgilityPack;

using FinalYearProject.Models;
namespace FinalYearProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ServicesDataEntities db = new ServicesDataEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult viewAdvertisers(int? id)
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


            var advertisers = (db.Advertisers).Include(a => a.Category).Include(a => a.Category1).Include(a => a.Language).OrderBy(a => a.Name).Skip((page_id - 1) * 20).Take(20);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(advertisers.ToList());

        }
        public ActionResult viewActions(int? id)
        {

            var actions = db.Actions.Where(a => a.AdvertiserID == id);

            return View(actions);

        }
        public ActionResult viewCommission(int? id)
        {

            var Commissions = db.Commissions.Where(a => a.ActionID == id);

            return View(Commissions);

        }


        public ActionResult viewProducts(int? id)
        {
            if (id == null)
            {
                id = 1;

            }
            var total_record = db.Products.Count();
            var total_pages = total_record / 20;
            var page_id = (int)id;

            if (page_id > total_pages)
            {
                return HttpNotFound();
            }

            var Products = (db.Products).Include(a => a.Advertiser).OrderBy(a => a.Name).Skip((page_id - 1) * 20).Take(20);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(Products.ToList());
        }






        public ActionResult viewOffers(int? id)
        {
            




            if (id == null)
            {
                id = 1;

            }
            var total_record = db.Offers.Count();
            var total_pages = total_record / 10;
            var page_id = (int)id;

            if (page_id > total_pages)
            {
                return HttpNotFound();
            }

            var Products = (db.Offers).Include(a => a.Promotion).Include(a => a.LinkType).Include(a => a.Language).Include(a => a.Category).Include(a => a.Advertiser).Include(a => a.OfferDetails).OrderBy(a =>a.CreateDate).Skip((page_id - 1) * 10).Take(10);
            ViewBag.id = page_id;
            ViewBag.totalpage = total_pages;

            return View(Products.ToList());
        }




        public ActionResult advertiserEdit(int? id)
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
        public ActionResult advertiserEdit([Bind(Include = "AdvertiserID,AccountStatus,SevenDayEpc,ThreeMonthEpc,LanguageID,Name,Url,RelationshipStatus,MobileTracking,NetworkRank,ParentCategoryID,ChildCategoryID,PerformanceIncentive,CreateDate,ModifyDate")] Advertiser advertiser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertiser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("viewAdvertiserDetail");
            }
            ViewBag.ParentCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ParentCategoryID);
            ViewBag.ChildCategoryID = new SelectList(db.Categories, "CategoryID", "Category1", advertiser.ChildCategoryID);
            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", advertiser.LanguageID);
            return View(advertiser);
        }
        public ActionResult productDetails(int? id)
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
        // GET: Products/Edit/5
        public ActionResult productEdit(int? id)
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
        public ActionResult productEdit([Bind(Include = "AdID,AdvertiserID,AdvertisorCategory,BuyUrl,CatalogID,Currency,Description,ImageUrl,InStock,ManufacturerSKU,Name,Price,RetailPrice,SalePrice,SKU,UPC,ISBN,ProductAutoKey,CreateDate,ModifyDate,ManufactureName")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("viewProductsDetail");
            }
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", product.AdvertiserID);
            return View(product);
        }
        public ActionResult offerDetails(int? id)
        {
            int? offeraproved;
            ViewBag.Added = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var offer = db.Offers.Where(a => a.OfferID == id).Include(a => a.OfferDetails);
            var offerapproved = db.OfferApproveds.Where(a=>a.OfferID==id).FirstOrDefault();
            if(offerapproved!=null)
            {
                offeraproved = offerapproved.OfferID;
                ViewBag.Added = offeraproved;
            }
          
            if (offer == null)
            {
                return HttpNotFound();
            }

            return View(offer.FirstOrDefault());
        }
        public ActionResult advertiserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var advertiser = db.Advertisers.Find(id);
            if (advertiser == null)
            {
                return HttpNotFound();
            }
            return View(advertiser);
        }
        public ActionResult offerEdit(int? id)
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
        public ActionResult offerEdit([Bind(Include = "AdvertiserID,CategoryID,ClickCommission,LanguageID,LeadCommission,Description,Destination,LinkID,LinkTypeID,PerformanceIncentive,PromotionEndDate,PromotionStartDate,PromotionID,CouponCode,RelationStatus,SalesCommission,SevenDayEPC,ThreeMonthEPC,ClickUrl,LinkName,OfferID,CreateDate,modfiyDate")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("viewOffers");
            }
            ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", offer.AdvertiserID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Category1", offer.CategoryID);

            ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", offer.LanguageID);
            ViewBag.LinkTypeID = new SelectList(db.LinkTypes, "LinkTypeID", "LinkType1", offer.LinkTypeID);
            ViewBag.PromotionID = new SelectList(db.Promotions, "PromotionID", "PromotionType", offer.PromotionID);
            return View(offer);
        }


        public ActionResult Keyword_cloud()
        {
            string query = "select top 30 document_count from sys.dm_fts_index_keywords( DB_ID('ServicesData'), OBJECT_ID('offer') ) where display_term!='End of file' and column_id=6 order by document_count desc";
            string query1 = "select top 30 display_term from sys.dm_fts_index_keywords( DB_ID('ServicesData'), OBJECT_ID('offer') ) where display_term!='End of file' and column_id=6 order by document_count desc";

            var doc_count = db.Database.SqlQuery<Int64>(query).ToList();
            var terms = db.Database.SqlQuery<string>(query1).ToList();
            keyword keywords = new keyword();
            keywords.document_count.AddRange(doc_count);
            keywords.terms.AddRange(terms);


            return View(keywords);
        }



        [HttpGet]
        public ActionResult Scrapper()
        {
            ViewBag.message = "";
            return View();
        }


        [HttpPost]
        public ActionResult Scrapper(FormCollection form)
        {
            string url = form["url"];
            string filename = form["filename"];
            ViewBag.message = "Page successfully Scraped";
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();

                HtmlDocument doc = htmlWeb.Load(url);

                var nodes = doc.DocumentNode.SelectNodes("//script|//style|//comment()");

                foreach (var node in nodes)
                    node.ParentNode.RemoveChild(node);
                //  string htmlOutput = doc.DocumentNode.OuterHtml;

                string text = doc.DocumentNode.SelectSingleNode("//body").InnerText;
                filename = @"E:\fyp\scrapped data\" + filename + ".txt";

                System.IO.File.WriteAllText(filename, text);
            }

            catch (Exception e)
            {
                ViewBag.message = "failed to scrap page";
            }

            return View();
        }
        public ActionResult Search(FormCollection fomr)
        {

            String selectedType = fomr["Selectedname"];
            String textBoxValue = fomr["SearchBox"];

            if (selectedType == "Product")
            {
                return RedirectToAction("productSearch", "Admin", new { keyValue = textBoxValue });
            }
            if (selectedType == "Offer")
            {
                return RedirectToAction("offerSearch", "Admin", new { keyValue = textBoxValue });
            }
            if (selectedType == "Advertiser")
            {
                return RedirectToAction("advertiserSearch", "Admin", new { keyValue = textBoxValue });
            }


            return View();
        }
        public ActionResult productSearch(string keyValue)
        {
            // value = "laptop";
            ViewBag.Key = keyValue;
            var product = db.Products.Where(a => a.Name.Contains(keyValue.ToString())).ToList().Take(20);
            //var product = db.Products.Take(10);
            return View(product);
        }
        public ActionResult offerSearch(string keyValue)
        {

            ViewBag.Key = keyValue;
            var offer = db.Offers.Where(a => a.LinkName.Contains(keyValue.ToString())).ToList();
            //var product = db.Products.Take(10);
            return View(offer);
        }
        public ActionResult advertiserSearch(string keyValue)
        {

            ViewBag.Key = keyValue;
            var advertiser = db.Advertisers.Where(a => a.Name.Contains(keyValue.ToString())).ToList().Take(20);
            //var product = db.Products.Take(10);
            return View(advertiser);
        }
        public ActionResult offerAdd(int id)
        {
            //string description = "April Sale! Save Sitewide $10 Off orders of $99 with code: APRIL10, $15 Off order of $199 with code: APRIL15, or $20 Off orders of $299 with code: APRIL20";
            ViewBag.status = null;
            int? offeraproved;
            ViewBag.Added = null;

            var offerapproved = db.OfferApproveds.Where(a => a.OfferID == id).FirstOrDefault();
            if (offerapproved != null)
            {
                offeraproved = offerapproved.OfferID;
                ViewBag.Added = offeraproved;
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            String Description = offer.Description.ToString();
            KeywordAnalysis KeynAalysis = new KeywordAnalysis();
            var aa = KeynAalysis.receiver(Description);
            aa.offerid = id;
            aa.offerdescription = Description;
            ViewBag.Description = Description;
            //ViewBag.AdvertiserID = new SelectList(db.Advertisers, "AdvertiserID", "AccountStatus", offer.AdvertiserID);
            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Category1", offer.CategoryID);
            //ViewBag.LanguageID = new SelectList(db.Languages, "LanguageID", "Languages", offer.LanguageID);
            //ViewBag.LinkTypeID = new SelectList(db.LinkTypes, "LinkTypeID", "LinkType1", offer.LinkTypeID);
            //ViewBag.PromotionID = new SelectList(db.Promotions, "PromotionID", "PromotionType", offer.PromotionID);
            return View(aa);

        }
        [HttpPost]
        public ActionResult offerAdd(KeywordAnalysis kanalysis)
        {

            int prmotionid = db.Promotions.FirstOrDefault().PromotionID;
            string sitewide = kanalysis.sitewide;
            string freeshiping = kanalysis.freeshipping;
                if(sitewide!=null)
            {
                var promotiontype = db.Promotions.Where(a => a.PromotionType == sitewide).First();
                prmotionid = promotiontype.PromotionID;
            }
            if(freeshiping!=null)
            {
                var promotiontype = db.Promotions.Where(a => a.PromotionType == freeshiping).First();
                prmotionid = promotiontype.PromotionID;
            }
         

            string enddate = kanalysis.end_date;
            string code = kanalysis.code;
            string description = kanalysis.offerdescription;
            int offerid = kanalysis.offerid;

            string saveamount = kanalysis.save_amount;
            string offamount = kanalysis.off_amount;

            

            string eligibleamount = kanalysis.eligible_amount;
                    

            var offer= db.Offers.Find(offerid);
            offer.PromotionEndDate = Convert.ToDateTime(enddate);
            offer.CouponCode = code;
            offer.PromotionID = prmotionid;
            db.Entry(offer).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                int a = 1;
            }
            // var offerApproved=db.of
            OfferApproved offerapproved = new OfferApproved();
            offerapproved.OfferID = offerid;
            offerapproved.EligibaleCriteria = eligibleamount;
            if (saveamount != null)
            {
                if (saveamount.Contains('%'))
                {
                    saveamount.Remove(saveamount.IndexOf('%'), 1);
                } 
                try {
                    offerapproved.Discount = Convert.ToDouble(saveamount);
                }
                catch(Exception e)
                {

                }
            }
            if (offamount != null)
            {
                if (offamount.Contains('%'))
                {
                    offamount.Remove(saveamount.IndexOf('%'), 1);
                }

                try {
                    offerapproved.Discount = Convert.ToDouble(offamount);
                }
                catch(Exception e)
                {

                }
            }
            db.OfferApproveds.Add(offerapproved);
            db.SaveChanges();
            ViewBag.status = "Added";
            ViewBag.Description = offer.Description;
            return View(kanalysis);
        }
        public ActionResult productSearchNametoID(string keyValue)
        {
            // value = "laptop";
            ViewBag.Key = keyValue;
            var Advertiser = db.Advertisers.Where(a => a.Name.Contains(keyValue.ToString())).FirstOrDefault();
            int advertiserID = Advertiser.AdvertiserID;
            var product = db.Products.Where(a => a.AdvertiserID == advertiserID).ToList();
            //var product = db.Products.Take(10);
            return View(product);
        }
        public ActionResult offerSearchNameToID(string keyValue)
        {

            ViewBag.Key = keyValue;
            var Advertiser = db.Advertisers.Where(a => a.Name.Contains(keyValue.ToString())).FirstOrDefault();
            int advertiserID = Advertiser.AdvertiserID;
            var offer = db.Offers.Where(a => a.AdvertiserID == advertiserID).ToList();
            //var product = db.Products.Take(10);
            return View(offer);
        }
        public ActionResult sort()
        {
            return View();
        }
        public ActionResult NER(string s)
        {
            nlp n = new nlp();
            ViewBag.NERList = n.getNER(s);
            return View();
        }
    }
    }
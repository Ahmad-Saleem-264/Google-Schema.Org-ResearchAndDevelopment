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
    public class CategoryController : Controller
    {
        private ServicesDataEntities db = new ServicesDataEntities();
        // GET: Category
        public ActionResult Index(int? id)
        {
            if(id==null)
            {
                id = 1;
            }
            int category_id =(int)id;
            var category = db.Categories.Where(a => a.CategoryID == category_id).FirstOrDefault().Category1;
            ViewBag.category_name = category;
            ViewBag.category_id = category_id;
            return View();
        }


        public ActionResult Offers_by_Category(int? id)
        {
            if(id==null)
            {
                id = 1;
            }
            int category_id = (int)id;

            var total_record = db.Offers.Where(a => a.CategoryID == category_id).Count();

            

            

            if (total_record<1)
            {
                return HttpNotFound();
            }

            var offers= db.Offers.Include(a => a.Category).Include(a => a.Advertiser).Include(a => a.Promotion).Where(a=>a.CategoryID==category_id).OrderBy(a => a.CreateDate).Take(10);
            var category = db.Categories.Where(a => a.CategoryID == category_id).FirstOrDefault().Category1;

            ViewBag.category_name = category;
            ViewBag.category_id = category_id;
           

            return View(offers);
        }

        public ActionResult Advertisers_by_Category(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            int category_id = (int)id;

            var total_record = db.Advertisers.Where(a => a.ChildCategoryID == category_id).Count();





            if (total_record < 1)
            {
                return HttpNotFound();
            }

            var advertisers = (db.Advertisers).Include(a => a.Category1).Where(a => !a.NetworkRank.Equals("new")).Where(a=>a.ChildCategoryID==category_id).OrderByDescending(a => a.NetworkRank).Take(20);
            var offers = db.Offers.Include(a => a.Category).Include(a => a.Advertiser).Include(a => a.Promotion).Where(a => a.CategoryID == category_id).OrderBy(a => a.CreateDate).Take(10);
            var category = db.Categories.Where(a => a.CategoryID == category_id).FirstOrDefault().Category1;

            ViewBag.category_name = category;
            ViewBag.category_id = category_id;

            return View(advertisers);
        }

        public ActionResult Products_by_Category(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            int category_id = (int)id;
            var offers = db.Offers.Include(a => a.Category).Include(a => a.Advertiser).Include(a => a.Promotion).Where(a => a.CategoryID == category_id).OrderBy(a => a.CreateDate).Take(10);
            var category = db.Categories.Where(a => a.CategoryID == category_id).FirstOrDefault().Category1;

            ViewBag.category_name = category;
            ViewBag.category_id = category_id;

            var total_record = db.Products.Include(a => a.Advertiser).Where(a => a.AdvertisorCategory.Contains(category)).Where(a => !(a.Advertiser.NetworkRank.Equals("new"))).Count();

            var products = db.Products.Include(a => a.Advertiser).Where(a => a.AdvertisorCategory.Contains(category.Replace("'",""))).Where(a => !(a.Advertiser.NetworkRank.Equals("new"))).OrderByDescending(a => a.Advertiser.NetworkRank).Take(18);



            if (total_record < 1)
            {
                return HttpNotFound();
            }
            return View(products);
        }

    }
}
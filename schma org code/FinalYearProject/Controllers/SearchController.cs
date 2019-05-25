using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        private ServicesDataEntities db = new ServicesDataEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(FormCollection fomr)
        {

            String selectedType = fomr["Selectedname"];
            String textBoxValue = fomr["SearchBox"];

            if (selectedType == "Product")
            {
                return RedirectToAction("productSearch", "Search", new { keyValue = textBoxValue });
            }
            if (selectedType == "Offer")
            {
                return RedirectToAction("offerSearch", "Search", new { keyValue = textBoxValue });
            }
            if (selectedType == "Advertiser")
            {
                return RedirectToAction("advertiserSearch", "Search", new { keyValue = textBoxValue });
            }


            return View();
        }
        public ActionResult advertiserSearch(string keyValue)
        {

            ViewBag.Key = keyValue;
            var advertiser = db.Advertisers.Where(a => a.Name.Contains(keyValue.ToString())).OrderByDescending(a => a.CreateDate).ToList();
            //var product = db.Products.Take(10);
            return View(advertiser);
        }
        public ActionResult productSearch(string keyValue)
        {
            // value = "laptop";
            ViewBag.Key = keyValue;
            var product = db.Products.Where(a => a.Name.Contains(keyValue.ToString())).OrderByDescending(a => a.CreateDate).ToList();
            //var product = db.Products.Take(10);
            return View(product);
        }
        public ActionResult offerSearch(string keyValue)
        {

            ViewBag.Key = keyValue;
            var offer = db.Offers.Where(a => a.LinkName.Contains(keyValue.ToString())).OrderByDescending(a => a.CreateDate).ToList();
            //var product = db.Products.Take(10);
            return View(offer);
        }
      
        public ActionResult productSearchNametoID(string keyValue)
        {
            // value = "laptop";
            ViewBag.Key = keyValue;
            var Advertiser = db.Advertisers.Where(a=> a.Name.Contains(keyValue.ToString())).FirstOrDefault();
            int advertiserID = Advertiser.AdvertiserID;
            var product = db.Products.Where(a => a.AdvertiserID==advertiserID).OrderByDescending(a => a.CreateDate).ToList();
            //var product = db.Products.Take(10);
            return View(product);
        }
        public ActionResult offerSearchNameToID(string keyValue)
        {

            ViewBag.Key = keyValue;
            var Advertiser = db.Advertisers.Where(a => a.Name.Contains(keyValue.ToString())).FirstOrDefault();
            int advertiserID = Advertiser.AdvertiserID;
            var offer = db.Offers.Where(a => a.AdvertiserID==advertiserID).OrderByDescending(a=>a.CreateDate).ToList();
            //var product = db.Products.Take(10);
            return View(offer);
        }
      
    }
}
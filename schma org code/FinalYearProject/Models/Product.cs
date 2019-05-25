//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalYearProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Nullable<int> AdID { get; set; }
        public Nullable<int> AdvertiserID { get; set; }
        public string AdvertisorCategory { get; set; }
        public string BuyUrl { get; set; }
        public string CatalogID { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string InStock { get; set; }
        public string ManufacturerSKU { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> RetailPrice { get; set; }
        public Nullable<double> SalePrice { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string ISBN { get; set; }
        public int ProductAutoKey { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ManufactureName { get; set; }
    
        public virtual Advertiser Advertiser { get; set; }
    }
}

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
    
    public partial class Offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offer()
        {
            this.OfferDetails = new HashSet<OfferDetail>();
        }
    
        public Nullable<int> AdvertiserID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<double> ClickCommission { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public string LeadCommission { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public Nullable<int> LinkID { get; set; }
        public Nullable<int> LinkTypeID { get; set; }
        public string PerformanceIncentive { get; set; }
        public Nullable<System.DateTime> PromotionEndDate { get; set; }
        public Nullable<System.DateTime> PromotionStartDate { get; set; }
        public Nullable<int> PromotionID { get; set; }
        public string CouponCode { get; set; }
        public string RelationStatus { get; set; }
        public string SalesCommission { get; set; }
        public string SevenDayEPC { get; set; }
        public string ThreeMonthEPC { get; set; }
        public string ClickUrl { get; set; }
        public string LinkName { get; set; }
        public int OfferID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> modfiyDate { get; set; }
    
        public virtual Advertiser Advertiser { get; set; }
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        public virtual LinkType LinkType { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual OfferApproved OfferApproved { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferDetail> OfferDetails { get; set; }
    }
}

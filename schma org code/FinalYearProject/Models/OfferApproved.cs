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
    
    public partial class OfferApproved
    {
        public int OfferID { get; set; }
        public Nullable<double> Discount { get; set; }
        public string EligibaleCriteria { get; set; }
    
        public virtual Offer Offer { get; set; }
    }
}

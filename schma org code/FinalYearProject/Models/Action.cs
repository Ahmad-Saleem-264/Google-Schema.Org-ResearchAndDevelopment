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
    
    public partial class Action
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Action()
        {
            this.Commissions = new HashSet<Commission>();
        }
    
        public int ActionID { get; set; }
        public string Name { get; set; }
        public Nullable<int> AdvertiserID { get; set; }
        public string Type { get; set; }
        public string Defaults { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Advertiser Advertiser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commission> Commissions { get; set; }
    }
}
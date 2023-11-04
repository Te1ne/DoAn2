//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderProduct()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int Id_OderPro { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public Nullable<int> ID_Account { get; set; }
        public Nullable<System.DateTime> TimeArrival { get; set; }
        public string Discount_Code { get; set; }

        public string Address {  get; set; }

        public Nullable<int> TotalQuantity { get; set; }
        public Nullable<int> Id_Store { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Store Store { get; set; }
    }
}

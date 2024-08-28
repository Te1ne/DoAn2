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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class OrderDetail
    {
        [Key]
        public int Id_Orderdt { get; set; }
        [ForeignKey("Id_Product")]
        public Nullable<int> Id_Product { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
        [ForeignKey("IdOrder")]
        public Nullable<int> IdOrder { get; set; }
        public string StateDelivery { get; set; }
        public string StatePayment { get; set; }

        public virtual OrderProduct OrderProduct { get; set; }
        public virtual Product Product { get; set; }
    }
}

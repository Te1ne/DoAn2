using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class Top_ViewModel
    {

        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }

        public string namePro { get; set; }

        public decimal? price { get; set; }
        public string descriptionPro { get; set; }

        public string imagePro { get; set; }
        public Product product { get; set; }
        public OrderDetail orderDetail { get; set; }
        public IEnumerable<Product> List { get; set; }

        public int? Top_10_Pro { get; set; }    
    }
}
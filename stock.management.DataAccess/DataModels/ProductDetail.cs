using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.management.DataAccess.DataModels
{
    public class ProductDetail
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public long Quantity { get; set; }
        public decimal Priceperunit { get; set; }

    }
}

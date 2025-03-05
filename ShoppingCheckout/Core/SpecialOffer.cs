using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCheckout.Core
{
    public class SpecialOffer
    {
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
    }
}

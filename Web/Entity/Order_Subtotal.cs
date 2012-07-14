using System;
using System.Collections.Generic;

namespace Web.Entity
{
    public class Order_Subtotal
    {
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}

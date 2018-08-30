using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ajax_Newtest
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public System.DateTime OrderDate { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
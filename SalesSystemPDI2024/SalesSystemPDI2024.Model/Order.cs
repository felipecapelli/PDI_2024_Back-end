using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class Order
    {
        public long OrderID { get; set; }
        public long CustomersSuppliersID { get; set; }
        public bool IsSale { get; set; }
        public DateTime OrderOpeningDate { get; set; }
        public DateTime OrderClosingDate { get; set; }
    }
}

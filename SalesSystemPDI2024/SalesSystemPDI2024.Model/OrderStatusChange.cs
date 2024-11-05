using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesSystemPDI2024.Model
{
    public class OrderStatusChange
    {
        public long OrderID { get; set; }
        public long StatusID { get; set; }
        public string ChangeDate { get; set; }
        public string Description { get; set; }
    }
}

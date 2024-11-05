using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesSystemPDI2024.Model
{
    public class Product
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public long BaseUnitOfMeasurementID { get; set; }
        public long BaseCurrencyID { get; set; }
        public decimal Price { get; set; }
    }
}

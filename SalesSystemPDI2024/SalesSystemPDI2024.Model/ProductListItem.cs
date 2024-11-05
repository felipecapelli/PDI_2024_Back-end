using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class ProductListItem
    {
        public long ProductListID { get; set; }
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public long UnitOfMeasurementUsedOnTheOrderID { get; set; }
        public long QuantityOfUnitMesasurement { get; set; }
        public long CurrencyUsedOnTheOrderID { get; set; }
        public decimal PricePerUnitOfMeasurement { get; set; }
        public long ShippingAddressID { get; set; }
        public long ParentOrSubsidiaryStokID { get; set; }
    }
}

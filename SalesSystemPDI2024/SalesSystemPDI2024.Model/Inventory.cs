using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class Inventory
    {
        public long InventoryID { get; set; }
        public long ProductID { get; set; }
        public decimal QuantityPerBaseUnityOfMeasurement { get; set; }
        public long ParentOrSubsidiaryStokID { get; set; }
    }
}

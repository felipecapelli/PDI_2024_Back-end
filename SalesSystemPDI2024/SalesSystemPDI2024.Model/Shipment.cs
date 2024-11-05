using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class Shipment
    {
        public long ShipmentID { get; set; }
        public long ProductListID { get; set; }
        public DateTime ExpectedDateOfShipment { get; set; }
        public DateTime ShipmentDepartureDate { get; set; }
        public DateTime ShipmentArrivalDate { get; set; }
    }
}

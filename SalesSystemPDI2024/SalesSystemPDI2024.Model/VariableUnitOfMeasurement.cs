using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class VariableUnitOfMeasurement
    {
        public long VariableUnitOfMeasurementID { get; set; }
        public string UnitName { get; set; }
        public string UnitAbbreviation { get; set; }
        public long BaseUnitOfMeasurementID { get; set; }
        public decimal QuantityOfBaseUnits { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesSystemPDI2024.Model
{
    public class CorporateEntity
    {
        public long CustomersSuppliersID { get; set; }
        public DateTime FoundationDate { get; set; }
        public string BusinessKindDescription { get; set; }
        public long LegalRepresentativeFiscalID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesSystemPDI2024.Model
{
    public class CurrencyQuotation
    {
        public long CurrencyQuotationID { get; set; }
        public string CurrencyName { get; set; }
        public string CurencyAbbreviation { get; set; }
        public long BaseCurrencyID { get; set; }
        public decimal QuantityOfBaseCurrency { get; set; }
        public DateTime QuotationDate { get; set; }
    }
}

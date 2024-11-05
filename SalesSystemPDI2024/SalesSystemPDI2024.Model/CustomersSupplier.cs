using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class CustomersSupplier
    {
        public long CustomersSuppliersID { get; set; }
        public string FiscalID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsCustomerRepresentative { get; set; }
        public bool IsSuplierRepresentative { get; set; }
        public bool IsParent { get; set; }
        public bool IsSubsidiary { get; set; }
        public bool IsParentRepresentative { get; set; }
        public bool IsSubsidiaryRepresentative { get; set; }
    }
}

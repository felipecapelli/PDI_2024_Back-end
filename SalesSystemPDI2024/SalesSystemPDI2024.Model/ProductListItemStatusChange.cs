using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class ProductListItemStatusChange
    {
        public long ProductListID { get; set; }
        public long StatusID { get; set; }
        public string ChangeDate { get; set; }
        public string Description { get; set; }
    }
}

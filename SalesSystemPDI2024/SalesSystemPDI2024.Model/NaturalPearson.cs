using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalesSystemPDI2024.Model
{
    public class NaturalPearson
    {
        public long CustomersSuppliersID { get; set; }
        public string IdentityCard { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
    }
}

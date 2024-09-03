using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemPDI2024.Model
{
    public class Address
    {
        public string AdressNickname { get; set; }
        public string AdressType { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string StateOrDistrict { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}

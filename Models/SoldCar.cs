using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paul_Andreea_Proiect.Models
{
    public class SoldCar
    {
        public int SellerID { get; set; }
        public int CarID { get; set; }
        public Seller Seller { get; set; }
        public Car Car { get; set; }
    }
}

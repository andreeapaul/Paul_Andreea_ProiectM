using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paul_Andreea_Proiect.Models.AutoViewModels
{
    public class SellerIndexData
    {
        public IEnumerable<Seller> Sellers { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

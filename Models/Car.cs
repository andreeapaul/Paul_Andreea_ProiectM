using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paul_Andreea_Proiect.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string PostTitle {get; set;}
        public string Brand { get; set; }
        public string Model { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price {get; set;}
         public ICollection <Order> Orders { get; set; }
        public ICollection <SoldCar> SoldCars { get; set; }

    }
}

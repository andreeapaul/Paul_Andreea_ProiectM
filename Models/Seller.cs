using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Paul_Andreea_Proiect.Models
{
    public class Seller
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Seller Name")]
        [StringLength(50)]
        public string SellerName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<SoldCar> SoldCars { get; set; }
    }
}

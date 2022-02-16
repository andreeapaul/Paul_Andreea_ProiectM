using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Paul_Andreea_Proiect.Models.AutoViewModels
{

    public class OrderGroup
       {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int CarCount { get; set; }

    }
}

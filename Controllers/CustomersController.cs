using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paul_Andreea_Proiect.Data;

namespace Paul_Andreea_Proiect.Controllers
{
    [Authorize(Policy = "SalesManager")]
    public class CustomersController : Controller
    {
        private readonly AutoTraderContext _context;
      
        public CustomersController(AutoTraderContext context)
        {
            _context = context;
        }
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Paul_Andreea_Proiect.Data;
using Paul_Andreea_Proiect.Models;
using Paul_Andreea_Proiect.Models.AutoViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Paul_Andreea_Proiect.Controllers
{
    [Authorize(Policy = "OnlySales")]
    public class SellersController : Controller
    {
        private readonly AutoTraderContext _context;

        public SellersController(AutoTraderContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public async Task<IActionResult> Index(int? id, int? carID)
        {
            var viewModel = new SellerIndexData();
            viewModel.Sellers = await _context.Sellers
            .Include(i => i.SoldCars)
            .ThenInclude(i => i.Car)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.SellerName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["SellerID"] = id.Value;
                Seller seller = viewModel.Sellers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Cars = seller.SoldCars.Select(s => s.Car);
            }
            if (carID != null)
            {
                ViewData["CarID"] = carID.Value;
                viewModel.Orders = viewModel.Cars.Where(
                x => x.ID == carID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SellerName,Adress")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = await _context.Sellers
            .Include(i => i.SoldCars).ThenInclude(i => i.Car)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }
            PopulateSoldCarData(seller);
            return View(seller);

        }
        private void PopulateSoldCarData(Seller seller)
        {
            var allCars = _context.Cars;
            var sellerCars = new HashSet<int>(seller.SoldCars.Select(c => c.CarID));
            var viewModel = new List<SoldCarData>();
            foreach (var car in allCars)
            {
                viewModel.Add(new SoldCarData
                {
                    CarID = car.ID,
                    PostTitle = car.PostTitle,
                    IsSold = sellerCars.Contains(car.ID)
                });
            }
            ViewData["Cars"] = viewModel;
        }



        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCars)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sellerToUpdate = await _context.Sellers
            .Include(i => i.SoldCars)
            .ThenInclude(i => i.Car)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Seller>(
            sellerToUpdate,
            "",
            i => i.SellerName, i => i.Adress))
            {
                UpdateSoldCars(selectedCars, sellerToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateSoldCars(selectedCars, sellerToUpdate);
            PopulateSoldCarData(sellerToUpdate);
            return View(sellerToUpdate);
        }
        private void UpdateSoldCars(string[] selectedCars, Seller sellerToUpdate)
        {
            if (selectedCars == null)
            {
                sellerToUpdate.SoldCars = new List<SoldCar>();
                return;
            }
            var selectedCarsHS = new HashSet<string>(selectedCars);
            var soldCars = new HashSet<int>
            (sellerToUpdate.SoldCars.Select(c => c.Car.ID));
            foreach (var car in _context.Cars)
            {
                if (selectedCarsHS.Contains(car.ID.ToString()))
                {
                    if (!soldCars.Contains(car.ID))
                    {
                        sellerToUpdate.SoldCars.Add(new SoldCar
                        {
                            SellerID =
                            sellerToUpdate.ID,
                            CarID = car.ID
                        });
                    }
                }
                else
                {
                    if (soldCars.Contains(car.ID))
                    {
                        SoldCar carToRemove = sellerToUpdate.SoldCars.FirstOrDefault(i
                       => i.CarID == car.ID);
                        _context.Remove(carToRemove);
                    }
                }
            }
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.ID == id);
        }
    }
}

using EShop.Domain.Domain;
using EShop.Repository;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        // GET: Apartments
        public IActionResult Index()
        {
            return View(_apartmentService.GetApartments());
        }

        // GET: Apartments/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ApartmentName,City,Description,Price_per_night,Rating")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _apartmentService.CreateNewApartment(apartment);
                return RedirectToAction("Index");
            }
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,ApartmentName,City,Description,Price_per_night,Rating")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _apartmentService.UpdateApartment(apartment);
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentService.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _apartmentService.DeleteApartment(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(Guid id)
        {
            return _apartmentService.GetApartmentById(id) != null;
        }
    }
}

using EShop.Domain.Domain;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IApartmentService _apartmentService;
        private readonly IBookingListService _bookingListService;

        public ReservationsController(IReservationService reservationService, IApartmentService apartmentService, IBookingListService bookingListService)
        {
            _reservationService = reservationService;
            _apartmentService = apartmentService;
            _bookingListService = bookingListService;
        }

        public IActionResult Index()
        {
            return View(_reservationService.GetReservations());
        }

        public IActionResult Details(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var reservation = _reservationService.GetReservationById(id);
            if(reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [Authorize]
        public IActionResult Create(Guid? apartmentId)
        {
            if(apartmentId == null)
            {
                ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName");
            }
            else
            {
                ViewData["ApartmentId"] = new SelectList(new List<Apartment> { _apartmentService.GetApartmentById(apartmentId)}, "Id", "ApartmentName");
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([Bind("Id,Check_in_date,ApartmentId")] Reservation reservation)
        {
            if(ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _reservationService.CreateNewReservation(user, reservation);
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var reservation = _reservationService.GetReservationById(id);
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_apartmentService.GetApartments(), "Id", "ApartmentName");
            return View(reservation);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Guid id, [Bind("Id,Check_in_date,ApartmentId")] Reservation reservation)
        {
           if(id != reservation.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _reservationService.UpdateReservation(reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = _reservationService.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult AddReservationToBookingList(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var res = _bookingListService.getProductInfo(id);
            if (res != null)
            {
                return View(res);
            }
            return View();
        }

        [HttpPost, ActionName("AddReservationToBookingList")]
        [Authorize]
        public IActionResult AddReservationToBookingList(BookReservation reservation)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _bookingListService.AddReservationToBookingList(userId, reservation);
            if (result != null)
            {
                return RedirectToAction("Index","BookingLists");
            }
            return View(reservation);
        }

    }
}

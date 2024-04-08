using EShop.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class BookingListsController : Controller
    {
        private readonly IBookingListService _bookingListService;

        public BookingListsController(IBookingListService bookingListService)
        {
            _bookingListService = bookingListService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_bookingListService.getAllReservations(userId));
        }

        [Authorize]
        public IActionResult DeleteFromList(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _bookingListService.DeleteReservation(userId, id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult BookNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _bookingListService.BookNow(userId);
            return RedirectToAction("Index");
        }

    }
}

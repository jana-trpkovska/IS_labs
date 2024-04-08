using EShop.Domain.Domain;
using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IBookingListService
    {
        BookingListDto getAllReservations(string userId);
        public BookingList AddReservationToBookingList(string userId, BookReservation reservation);
        public Boolean DeleteReservation(string userId, Guid? Id);
        public Boolean BookNow(string userId);
        public BookReservation getProductInfo(Guid Id);



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class BookReservation : BaseEntity
    {

        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public Guid BookingListId { get; set; }
        public BookingList? BookingList { get; set; }

        public int NumberOfNights { get; set; }
    }
}

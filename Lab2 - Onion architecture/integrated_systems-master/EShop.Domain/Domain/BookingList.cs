using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class BookingList : BaseEntity
    {
        public string? UserId { get; set; }
        public BookingApplicationUser? User { get; set; }
        public virtual ICollection<BookReservation>? BookedReservations { get; set; }
    }
}

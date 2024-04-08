using EShop.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DTO
{
    public class BookingListDto
    {
        public List<BookReservation> BookedReservations { get; set; }
        public int TotalPrice { get; set; }
    }
}

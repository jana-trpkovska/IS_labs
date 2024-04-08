using EShop.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IReservationService
    {
        public List<Reservation> GetReservations();
        public Reservation GetReservationById(Guid? id);
        public Reservation CreateNewReservation(string userId, Reservation reservation);
        public Reservation UpdateReservation(Reservation reservation);
        public Reservation DeleteReservation(Guid id);
    }
}

using EShop.Domain.Domain;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IRepository<Reservation> repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public Reservation CreateNewReservation(string userId, Reservation reservation)
        {
            var user = _userRepository.Get(userId);
            reservation.User = user;
            return _repository.Insert(reservation);
        }

        public Reservation DeleteReservation(Guid id)
        {
            return _repository.Delete(GetReservationById(id));

        }

        public Reservation GetReservationById(Guid? id)
        {
            return _repository.Get(id);
        }

        public List<Reservation> GetReservations()
        {
            return _repository.GetAll().ToList();
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            return _repository.Update(reservation);
        }
    }
}

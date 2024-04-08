using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EShop.Service.Implementation
{
    public class BookingListService : IBookingListService
    {
        private readonly IRepository<BookingList> _bookingListRepository;
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IUserRepository _userRepository;

        public BookingListService(IRepository<BookingList> bookingListRepository, IUserRepository userRepository, IRepository<Reservation> reservationRepository)
        {
            _bookingListRepository = bookingListRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
        }

        public BookingList AddReservationToBookingList(string userId, BookReservation reservation)
        {
            if (userId != null)
            {
                var user = _userRepository.Get(userId);
                var bookingList = user?.BookingList;

                if (bookingList != null)
                {
                    bookingList.BookedReservations.Add(
                        new BookReservation
                        {
                            ReservationId = reservation.Id,
                            Reservation = reservation.Reservation,
                            BookingList = bookingList,
                            BookingListId = bookingList.Id,
                            NumberOfNights = reservation.NumberOfNights
                        });
                    return _bookingListRepository.Update(bookingList);
                }
            }
            return null;

        }

        public bool BookNow(string userId)
        {
            if(userId != null)
            {
                var user = _userRepository.Get(userId);

                var bookingList = user?.BookingList;
                bookingList.BookedReservations.Clear();
                _bookingListRepository.Update(bookingList);

                return true;
            }
            return false;
        }

        public bool DeleteReservation(string userId, Guid? Id)
        {
            if(userId != null)
            {
                var user = _userRepository.Get(userId);
                var reservation = user.BookingList.BookedReservations.First(x => x.Id == Id);
                user.BookingList.BookedReservations.Remove(reservation);
                _bookingListRepository.Update(user.BookingList);
                return true;
            }

            return false;
        }

        public BookingListDto getAllReservations(string userId)
        {
            if(userId != null)
            {
                var user = _userRepository.Get(userId);

                var allReservations = user.BookingList.BookedReservations.ToList();
                var totalPrice = 0;

                if(allReservations.Count > 0)
                {
                    foreach(var reservation in allReservations)
                    {
                        totalPrice += reservation.NumberOfNights * reservation.Reservation.Apartment.Price_per_night;
                    }
                }

                var dto = new BookingListDto
                {
                    BookedReservations = allReservations,
                    TotalPrice = totalPrice
                };

                return dto;
            }

            return new BookingListDto
            {
                BookedReservations = new List<BookReservation>(),
                TotalPrice = 0
            };
        }

        public BookReservation getProductInfo(Guid Id)
        {
            var reservation = _reservationRepository.Get(Id);
            if (reservation != null)
            {
                var model = new BookReservation
                {
                    Reservation = reservation,
                    ReservationId = Id,
                    NumberOfNights = 1
                };
                return model;
            }
            return null;

        }
    }
}

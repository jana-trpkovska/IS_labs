using EShop.Domain.Identity;
using EShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<BookingApplicationUser> entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            this.entities = _context.Set<BookingApplicationUser>();
        }

        public void Delete(BookingApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public BookingApplicationUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.BookingList)
                .Include(z => z.BookingList.BookedReservations)
                .Include("BookingList.BookedReservations.Reservation")
                .Include("BookingList.BookedReservations.Reservation.Apartment")
                .First(s => s.Id == strGuid);
        }

        public IEnumerable<BookingApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(BookingApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(BookingApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _context.SaveChanges();

        }
    }
}


using EShop.Domain.Domain;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<BookingApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<BookingList> BookingLists { get; set; }
        public virtual DbSet<BookReservation> BookReservations { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheatreShows.Web.Models;

namespace TheatreShows.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TheatreShows.Web.Models.TheatreShow> TheatreShow { get; set; } = default!;
        public DbSet<TheatreShows.Web.Models.Ticket> Ticket { get; set; } = default!;
    }
}

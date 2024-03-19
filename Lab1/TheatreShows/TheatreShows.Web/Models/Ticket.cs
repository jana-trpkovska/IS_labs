using System.ComponentModel.DataAnnotations;

namespace TheatreShows.Web.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public double Price { get; set; }
        public Guid? TheatreShowId { get; set; }
        public TheatreShow? TheatreShow { get; set; }
        public User? User { get; set; }
    }
}

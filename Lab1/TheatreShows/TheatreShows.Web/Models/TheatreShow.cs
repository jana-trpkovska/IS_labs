using System.ComponentModel.DataAnnotations;

namespace TheatreShows.Web.Models
{
    public class TheatreShow
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}

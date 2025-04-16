using System.ComponentModel.DataAnnotations;

namespace JobPortalBackend.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int JobId { get; set; }
    }
}

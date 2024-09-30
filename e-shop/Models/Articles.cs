using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace e_shop.Models
{
    public class Articles
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        public DateTime? DateAchat { get; set; }
        public string Title { get; set; }

        public double Price { get; set; }
    }
}

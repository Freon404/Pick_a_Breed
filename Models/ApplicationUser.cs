using Microsoft.AspNetCore.Identity;

namespace Pick_a_Breed.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Breed>? Favourites { get; set; }
    }
}

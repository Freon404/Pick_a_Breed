using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pick_a_Breed.Models;

namespace Pick_a_Breed.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Pick_a_Breed.Models.Breed>? Breed { get; set; }
        public DbSet<Pick_a_Breed.Models.Feature>? Feature { get; set; }
    }
}
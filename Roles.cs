using Microsoft.AspNetCore.Identity;
using Pick_a_Breed.Models;

namespace Pick_a_Breed
{
    public static class Roles
    {
        public  static void Create(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            CreateRoles(roleManager);
            CreateUsers(userManager);
        }
        
        public  static void CreateUsers(UserManager<ApplicationUser> userManager) 
        {
            if (userManager.FindByNameAsync("admin@admin.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
                var result = userManager.CreateAsync(user, "Pa$$word1").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        public  static void CreateRoles(RoleManager<IdentityRole> roleManager) 
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result) 
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}

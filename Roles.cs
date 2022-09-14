using Microsoft.AspNetCore.Identity;

namespace Pick_a_Breed
{
    public static class Roles
    {
        public  static void Create(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            CreateRoles(roleManager);
            CreateUsers(userManager);
        }
        
        public  static void CreateUsers(UserManager<IdentityUser> userManager) 
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
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
                roleManager.CreateAsync(role);
            }
        }
    }
}

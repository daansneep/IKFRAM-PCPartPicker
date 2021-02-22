using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<Admin> userManager)
        {
            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(new Admin
                {
                    DisplayName = "Daan Sneep",
                    UserName = "daansneep",
                    Email = "daansneep@hotmail.nl"
                }, "Pa$$w0rd");
            }
        }
    }
}
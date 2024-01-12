using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Nasr",
                    Email = "ahmed.nasr@linkdev.com",
                    UserName = "ahmed.nasr",
                    Address = new Address()
                    {
                        FirstName = "Ahmed",
                        LastName = "Nasr",
                        Country = "Egypt",
                        City = "Giza",
                        Street = "10 Tahrir St."
                    }
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}

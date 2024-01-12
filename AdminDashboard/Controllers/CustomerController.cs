using AdminDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace AdminDashboard.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CustomerController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Users = await userManager.Users.Select(U => new UserViewModel
            {
                Id = U.Id,
                UserName = U.UserName,
                Email = U.Email,
                DisplayName = U.DisplayName,
                Roles = userManager.GetRolesAsync(U).Result
            }).ToListAsync();
            return View(Users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var Customer = await userManager.FindByIdAsync(id);
            var AllRoles = await roleManager.Roles.ToListAsync();
            var ViewModel = new UserRolesViewModel()
            {
                UserId = Customer.Id,
                UserName=Customer.DisplayName,
                Roles = AllRoles.Select(R => new RoleViewModel
                {
                    Id = R.Id,
                    Name = R.Name,
                    IsSelected = userManager.IsInRoleAsync(Customer, R.Name).Result
                }).ToList()
             };
                             return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(UserRolesViewModel model)
        {
            var User = await userManager.FindByIdAsync(model.UserId);
            var UserRoles = await userManager.GetRolesAsync(User); 
            foreach(var Role in model.Roles)
            {
                if (UserRoles.Any(R => R == Role.Name) && !Role.IsSelected)
                    await userManager.RemoveFromRoleAsync(User, Role.Name);
                if (!UserRoles.Any(R => R == Role.Name) && Role.IsSelected)
                    await userManager.AddToRoleAsync(User, Role.Name);
            }
            return RedirectToAction(nameof(Index));
        }
}   }

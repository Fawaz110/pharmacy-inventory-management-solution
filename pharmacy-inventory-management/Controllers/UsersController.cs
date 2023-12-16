using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharmacy_inventory_management.Models;

namespace pharmacy_inventory_management.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users =  await _userManager.Users.ToListAsync();
            List<string> roles = new List<string>();
            foreach (var user in users)
            {
                foreach (var role in await _roleManager.Roles.ToListAsync())
                    if(await _userManager.IsInRoleAsync(user,role.Name))
                        roles.Add(role.Name);
            }
            ViewData["roles"] = roles;
            ViewData["users"] = users;
            return View(new UserVM());
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
                await _userManager.DeleteAsync(user);
            
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserVM user)
        {
            if (ModelState.IsValid)
            {
                var account = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                var result = await _userManager.CreateAsync(account, user.Password);
                if (result.Succeeded)
                {
                    var addToRole = await _userManager.AddToRoleAsync(account, user.Role);
                    if(addToRole.Succeeded)
                    {

                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            var users = await _userManager.Users.ToListAsync();
            List<string> roles = new List<string>();
            foreach (var item in users)
            {
                foreach (var role in await _roleManager.Roles.ToListAsync())
                    if (await _userManager.IsInRoleAsync(item, role.Name))
                        roles.Add(role.Name);
            }
            ViewData["roles"] = roles;
            ViewData["users"] = users;

            return View(user);
        }
    }
}

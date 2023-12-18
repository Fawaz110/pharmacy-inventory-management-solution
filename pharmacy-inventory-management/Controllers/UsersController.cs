using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharmacy_inventory_management.Models;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml.Linq;

namespace pharmacy_inventory_management.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            List<string> roles = new List<string>();
            foreach (var user in users)
            {
                foreach (var role in await _roleManager.Roles.ToListAsync())
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                        roles.Add(role.Name);
            }
            ViewData["roles"] = roles;
            ViewData["users"] = users;
            return View(new UserVM());
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                /* ActionPerformed, details, adminName */
                var admin = await _userManager.GetUserAsync(User);
                var addProcessTrancking = new WorkflowTracking
                {
                    AdminId = admin.Id,
                    AdminName = admin.UserName,
                    ActionPerformed = $"{admin.UserName} removed {user.UserName}",
                    Details = $"{user.UserName} is no longer auhenticated to login to the system."
                };

                _unitOfWork.UserRepository.AddTrackingRecord(addProcessTrancking);

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }

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
                    if (addToRole.Succeeded)
                    {
                        // here admin must be applicationUser "UserName = Fawaz"
                        var admin = await _userManager.GetUserAsync(User);
                        var addProcessTrancking = new WorkflowTracking
                        {
                            AdminId = admin.Id,
                            AdminName = admin.UserName,
                            ActionPerformed = $"{admin.UserName} added {account.UserName}",
                            Details = $"{account.UserName} has been added to the system, as {user.Role}."
                        };

                        _unitOfWork.UserRepository.AddTrackingRecord(addProcessTrancking);

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

        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if(user != null)
                {
                    var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                    if(isCorrectPassword)
                    {
                        var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);
                        if (signInResult.Succeeded)
                        {
                            var loggedInUser = User;
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                }

                ModelState.AddModelError("incorrect", "Incorrect email or password");
                ViewData["incorrect"] = "Incorrect email or password";
            }

            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}

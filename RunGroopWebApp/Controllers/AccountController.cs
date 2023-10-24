using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM  )
        {
            if(!ModelState.IsValid)
            {

                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null) {
                //user is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    //password cerrect, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race"); 
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong credential. Please, try again";
                return View(loginVM);
            }
            //user not found
            TempData["Error"] = "Wrong credential. Please try again";
            return View(loginVM);
        }
    }
}

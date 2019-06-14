using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookKeep.ViewModels;
using BookKeep.Models;
using BookKeep.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookKeep.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IDAL dal;
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IDAL dataaccess, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dal = dataaccess;
            this.authenticationSchemeProvider = authenticationSchemeProvider;
        }
        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            var allSchemeProvider = (await authenticationSchemeProvider.GetAllSchemesAsync())                 .Select(n => n.DisplayName).Where(n => !String.IsNullOrEmpty(n));
            LogInViewModel vm = new LogInViewModel
            {
                FacebookProvider = allSchemeProvider.FirstOrDefault()
            };             ViewBag.Title = "Log In";

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel vm)
        {
            ViewBag.Title = "Log In";
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email_id, vm.Password, vm.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "LogIn Failed");
            return View(vm);
        }

        public IActionResult SignIn(String provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            //List<State> statelist = new List<State>();
            RegisterViewModel location = new RegisterViewModel
            {
                States = dal.StateList(),
                InterestRank = dal.InterestList()
            };

            return View(location);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.States = dal.StateList();
                vm.InterestRank = dal.InterestList();
                return View(vm);
            }
            int locationid = dal.AddLocation(Convert.ToInt32(vm.ZipCode), vm.SelectedState, (vm.City).ToUpper());
            var user = new User { Id=vm.Email_id, UserName = vm.Email_id, Email = vm.Email_id, First_name = vm.First_name, Last_name = vm.Last_name, 
            PhoneNumber = vm.Mobile_number, Age = vm.Age, CreatedTimestamp= DateTime.Now, LocationId = locationid};
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                dal.AddInterest(vm.NightVentures, vm.FoodVentures, vm.ArtandCultureVentures, vm.OutDoors, vm.Email_id);
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            vm.InterestRank = dal.InterestList();
            vm.States = dal.StateList();
            return View(vm);
        }
       

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
           // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

       

    }
}

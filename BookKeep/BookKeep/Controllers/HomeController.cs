using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookKeep.Models;
using BookKeep.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BookKeep.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDAL dal;
        protected UserManager<User> UserManager { get; set; }

        public HomeController(IDAL dataaccess, UserManager<User> userManager)
        {
            UserManager = userManager;
            dal = dataaccess;
        }

    public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Trip()
        {
            TripViewModel vm = new TripViewModel();
            var user = await UserManager.GetUserAsync(HttpContext.User);
            String userId = user?.Email;
            vm.Trips = dal.GetTrips(userId);
            if(vm.Trips.Count != 0)
            {
                vm.CurrTrip = vm.Trips.FirstOrDefault();
                vm.Trips.Remove(vm.Trips.FirstOrDefault());
            }

            return View(vm);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

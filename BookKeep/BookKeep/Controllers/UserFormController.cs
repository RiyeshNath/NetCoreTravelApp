using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookKeep.ViewModels;
using BookKeep.FunctionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookKeep.Models;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookKeep.Controllers
{
    [Authorize]
    public class UserFormController : Controller
    {
        public object ScriptManager { get; private set; }
        public object ClientScript { get; private set; }
        protected UserManager<User> UserManager { get; set; }
        private readonly IDAL dal;
        private readonly HelperClass help = new HelperClass();
        // GET: /<controller>/
        public UserFormController(IDAL dataaccess, UserManager<User> userManager)
        {
            UserManager = userManager;
            dal = dataaccess;
        }

        [HttpGet]
        public IActionResult AddTrip()
        {
            //List<State> statelist = new List<State>();
            AddTripViewModel trip = new AddTripViewModel();

            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrip(AddTripViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                var user = await UserManager.GetUserAsync(HttpContext.User);
                String userId = user?.Email;
                if (!dal.CheckTripDate(vm.StartDate, vm.EndDate, userId))
                {
                    return View(vm);
                }
                var client = new HttpClient();
                StringBuilder sb = new StringBuilder();
                sb.Append("https://maps.googleapis.com/maps/api/geocode/json?address=");
                sb.Append(vm.City);
                sb.Append(',');
                sb.Append(vm.Country);
                sb.Append("&key=AIzaSyA_Pvo7XDp6VZt8NTSZ_URxxqpUKaypOtY");
                var uri = new Uri(sb.ToString());
                var response = await client.GetStringAsync(uri);
                RootObject content = JsonConvert.DeserializeObject<RootObject>(response);
                if (content.results.Count == 0)
                {
                    return View(vm);
                }
                else 
                {
                    int countryid = 0;
                    for(int i = 0; i < content.results[0].address_components.Count(); i++) {
                        if (content.results[0].address_components[i].types[0].Equals("country"))
                        {
                            countryid = i;
                            break;
                        }
                    }
                    if (!vm.Country.ToUpper().Equals((content.results[0].address_components[countryid].long_name).ToUpper()) || !vm.City.ToUpper().Equals((help.TranslateAccent(content.results[0].address_components[0].long_name)).ToUpper()))
                    {
                        return View(vm);
                    }
                    else
                    {
                        int locationid = dal.AddTravelLocation(content.results[0].geometry.location.lat, content.results[0].geometry.location.lng, 
                        content.results[0].address_components[0].long_name, content.results[0].address_components[countryid].long_name);

                        dal.AddUserTrip(locationid, vm.TripName, userId, vm.StartDate, vm.EndDate, vm.Budget);

                    }

                }
               
            }catch(Exception ex)
            {
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}

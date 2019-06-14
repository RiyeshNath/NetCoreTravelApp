using System;
using System.Collections.Generic;
using BookKeep.Data;
using System.Linq;
using System.Data.SqlClient;
using BookKeep.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
namespace BookKeep.Models
{
    public class DAL: IDAL
    {
        private readonly BookKeepDBContext db;

        public DAL(BookKeepDBContext dBContext)
        {
           this.db = dBContext;
        }
        public List<State> StateList()
        {
            var list = (from state in db.States select state).ToList();
            list.Insert(0, new State { StateId = 0, StateName = "Select" });
            return list;
        }
        public IEnumerable<int> InterestList()
        {
            List<int> list = new List<int>();
            for(int i = 1; i <= 5; i++)
            {
                list.Add(i);
            }
            return list;
        }
        public int AddLocation(int zip, int stateId, string city)
        {
            int locationId;
            int cityId = AddCity(stateId, city);
            var list = (from a in db.User_Locations
                        join b in db.Cities on a.CityId equals b.CityId
                        where a.ZipCode.Equals(zip) & b.CityName.Equals(city)
                        select a).SingleOrDefault();
            if (list == null)
            {
                UserLocation location = new UserLocation
                {
                    CityId = cityId,
                    ZipCode = zip,

                };
                db.User_Locations.Add(location);
                db.SaveChanges();
                list = (from UserLocation in db.User_Locations
                        join cit in db.Cities on UserLocation.CityId equals cit.CityId
                        where UserLocation.ZipCode.Equals(zip) & cit.CityName.Equals(city)
                        select UserLocation).SingleOrDefault();
                return list.LocationId;
            }
            else
            {
                locationId = list.LocationId;
            }
            return locationId;
        }
        public int AddCity(int stateId, string cityName)
        {
            var val = (from city in db.Cities where city.StateId.Equals(stateId) && city.CityName.Equals(cityName) select city).FirstOrDefault();
            if (val == null)
            {
                City newCity = new City
                {
                    CityName = cityName,
                    StateId = stateId,

                };
                db.Cities.Add(newCity);
                db.SaveChanges();
                val = (from city in db.Cities where city.StateId.Equals(stateId) && city.CityName.Equals(cityName) select city).FirstOrDefault();
                return val.CityId;
            }
            else
            {
                return val.CityId;
            }
        }
        public int AddTravelLocation(double lat, double lon, String city, String country)
        {
            var val = (from travelloc in db.TravelLocations
                       where travelloc.Latitude.Equals(lat) && travelloc.Logitude.Equals(lon) &&
                       travelloc.City.Equals(city.ToUpper()) && travelloc.Country.Equals(country.ToUpper())
                       select travelloc).FirstOrDefault();
            if (val == null)
            {
                TravelLocation newLocation = new TravelLocation
                {
                    City = city,
                    Country = country,
                    Logitude = lon,
                    Latitude = lat,
                };
                db.TravelLocations.Add(newLocation);
                db.SaveChanges();
                val = (from travelloc in db.TravelLocations
                       where travelloc.Latitude.Equals(lat) && travelloc.Logitude.Equals(lon) &&
                       travelloc.City.Equals(city.ToUpper()) && travelloc.Country.Equals(country.ToUpper())
                       select travelloc).FirstOrDefault();
                return val.LocationId;
            }
            else
            {
                return val.LocationId;
            }

        }
        public void AddUserTrip(int locationid, String tripName, String userId, DateTime startDate, DateTime endDate, double budget)
        {
            UserTrip newTrip = new UserTrip
            {
                LocationId = locationid,
                UserId = userId,
                TripName = tripName,
                StartDate = startDate,
                EndDate = endDate,
                Budget = budget,
                Spent = 0.00,
            };
            db.UserTrips.Add(newTrip);
            db.SaveChanges();

        }
        public void AddInterest(int nightVentures, int foodVentures, int artandCultureVentures, int outDoors, String userId)
        {
            Interest interest = new Interest()
            {
                NightVentures = nightVentures,
                FoodVentures = foodVentures,
                ArtandCultureVentures = artandCultureVentures,
                OutDoors = outDoors,
                UserId = userId,
            };
            db.Interests.Add(interest);
            db.SaveChanges();

        }
        public bool CheckTripDate(DateTime startDate, DateTime endDate, String userId)
        {
            bool checkDate = false;
            var userTrip = (from trip in db.UserTrips
                            where userId.Equals(trip.UserId) && ((startDate >= trip.StartDate && endDate <= trip.EndDate) ||
            (startDate <= trip.StartDate && endDate >= trip.StartDate) || (startDate <= trip.EndDate && endDate >= trip.EndDate))
                            select trip).FirstOrDefault();
            if(userTrip == null)
            {
                checkDate = true;
            }
            return checkDate;
        }
        public List<UserTrip> GetTrips(String userId)
        {
            List<UserTrip> userTrip = (from trip in db.UserTrips
                            where userId.Equals(trip.UserId) && (trip.EndDate >= DateTime.Now)
                            orderby trip.StartDate ascending select trip).ToList();
            return userTrip;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookKeep.Models
{
    public interface IDAL
    {
        List<State> StateList();
        int AddCity(int stateId, string cityName);
        int AddLocation(int zip, int stateId, string city);
        IEnumerable<int> InterestList();
        int AddTravelLocation(double lat, double lon, String city, String country);
        void AddUserTrip(int locationid, String tripName, String userId, DateTime startDate, DateTime endDate, double budget);
        void AddInterest(int nightVentures, int foodVentures, int artandCultureVentures, int outDoors, String userId);
        bool CheckTripDate(DateTime startDate, DateTime endDate, String userId);
        List<UserTrip> GetTrips(String userId);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using BookKeep.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookKeep.Data
{
    public class BookKeepDBContext:IdentityDbContext<User>
    {
        public BookKeepDBContext(DbContextOptions<BookKeepDBContext> options)
      : base(options)
        { }

       //public DbSet<User> Users { get; set; }
        //public DbSet<Lending> Lendings { get; set; }
        public DbSet<UserLocation> User_Locations { get; set; }
        //public DbSet<Wallet> Wallets { get; set; }
        //public DbSet<Friendship> Friendships { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<TravelLocation> TravelLocations { get; set; }
        public DbSet<TripPlan> TripPlans { get; set; }
        public DbSet<Interest> Interests { get; set; }

    }
}

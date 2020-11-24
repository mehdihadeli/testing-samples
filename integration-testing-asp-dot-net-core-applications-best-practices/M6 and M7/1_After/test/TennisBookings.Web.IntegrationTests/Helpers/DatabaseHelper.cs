using System;
using System.Linq;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        public static void InitialiseDbForTests(TennisBookingDbContext dbContext)
        {
            dbContext.Users.Add(new TennisBookingsUser
            {
                Id = Guid.NewGuid().ToString(),
                NormalizedUserName = "TEST",
                NormalizedEmail = "TEST@EXAMPLE.COM",
                Member = new Member {Id = 1, Forename = "Test", Surname = "User"}
            });
            
            dbContext.SaveChanges();
        }

        public static void ResetDbForTests(TennisBookingDbContext dbContext)
        {
            var members = dbContext.Members.ToArray();
            var users = dbContext.Users.ToArray();
            var bookings = dbContext.CourtBookings.ToArray();

            dbContext.Members.RemoveRange(members);
            dbContext.Users.RemoveRange(users);
            dbContext.CourtBookings.RemoveRange(bookings);

            dbContext.SaveChanges();

            InitialiseDbForTests(dbContext);
        }
    }
}

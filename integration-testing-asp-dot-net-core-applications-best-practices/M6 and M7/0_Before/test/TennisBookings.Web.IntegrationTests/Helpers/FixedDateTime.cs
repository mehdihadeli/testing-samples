using System;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.IntegrationTests.Helpers
{
    public class FixedDateTime : IDateTime
    {
        public static DateTime UtcNow => new DateTime(2020, 04, 15, 10, 00, 00);

        public DateTime DateTimeUtc => UtcNow;
    }
}

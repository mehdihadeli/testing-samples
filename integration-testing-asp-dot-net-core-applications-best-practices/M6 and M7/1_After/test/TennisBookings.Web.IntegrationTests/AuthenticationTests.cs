using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TennisBookings.Web.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AuthenticationTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            _factory = factory;
        }

        [Theory]
        [InlineData("/Admin")]
        [InlineData("/Admin/Staff/Add")]
        [InlineData("/Admin/Courts/Bookings/Upcoming")]
        [InlineData("/Admin/Courts/Booking/1/Cancel")]
        [InlineData("/Admin/Courts/Maintenance/Upcoming")]
        [InlineData("/FindAvailableCourts")]
        [InlineData("/BookCourt/1")]
        [InlineData("/Bookings")]
        public async Task Get_SecurePageRedirectsAnUnauthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/identity/account/login", response.Headers.Location.OriginalString, StringComparison.OrdinalIgnoreCase);
        }
    }
}

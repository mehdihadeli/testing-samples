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
    }
}

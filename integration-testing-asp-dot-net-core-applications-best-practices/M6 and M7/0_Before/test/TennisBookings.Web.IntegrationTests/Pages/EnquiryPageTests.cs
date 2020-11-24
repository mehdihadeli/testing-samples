using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TennisBookings.Web.IntegrationTests.Pages
{
    public class EnquiryPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public EnquiryPageTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
    }
}

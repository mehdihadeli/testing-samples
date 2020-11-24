using System.Threading.Tasks;
using Xunit;

namespace TennisBookings.Web.IntegrationTests.Pages
{
    public class HomePageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public HomePageTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }        
    }
}

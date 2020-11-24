using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Web.External;
using TennisBookings.Web.IntegrationTests.Fakes;
using TennisBookings.Web.IntegrationTests.Helpers;
using TennisBookings.Web.Services;

namespace TennisBookings.Web.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Production");
            
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IWeatherApiClient, FakeWithDataWeatherApiClient>();
                services.AddSingleton<IDateTime, FixedDateTime>();
            });
        }
    }
}

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Data;
using TennisBookings.Web.IntegrationTests.Helpers;

namespace TennisBookings.Web.IntegrationTests.Pages
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder WithMemberUser(this IWebHostBuilder builder)
        {
            return builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication("Test")
                    .AddScheme<TestAuthenticationSchemeOptions, TestAuthenticationHandler>(
                        "Test", options => options.Role = "Member");
            });
        }

        public static IWebHostBuilder ConfigureTestDatabase(this IWebHostBuilder builder, Action<TennisBookingDbContext> configure)
        {
            return builder.ConfigureTestServices(services =>
            {
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TennisBookingDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<BookingsPageTests>>();

                try
                {
                    configure(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred setting up the " +
                                        "database for the test. Error: {Message}", ex.Message);
                }
            });
        }
    }
}


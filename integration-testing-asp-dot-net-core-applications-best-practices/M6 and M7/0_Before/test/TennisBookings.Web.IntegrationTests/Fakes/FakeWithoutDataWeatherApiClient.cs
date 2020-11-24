using System.Threading;
using System.Threading.Tasks;
using TennisBookings.Web.External;
using TennisBookings.Web.External.Models;

namespace TennisBookings.Web.IntegrationTests.Fakes
{
    public class FakeWithoutDataWeatherApiClient : IWeatherApiClient
    {
        public Task<WeatherApiResult> GetWeatherForecastAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((WeatherApiResult)null);
        }
    }
}

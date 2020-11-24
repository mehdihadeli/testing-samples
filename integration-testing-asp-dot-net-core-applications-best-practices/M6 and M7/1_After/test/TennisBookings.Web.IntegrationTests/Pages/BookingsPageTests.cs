using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using TennisBookings.Web.Data;
using TennisBookings.Web.IntegrationTests.Helpers;
using Xunit;

namespace TennisBookings.Web.IntegrationTests.Pages
{
    public class BookingsPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BookingsPageTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/bookings");
            _factory = factory;
        }

        [Fact]
        public async Task NoBookingsTableOnPage_WhenUserHasNoBookings()
        {
            var client = _factory.CreateClientWithMemberAndDbSetup(db => DatabaseHelper.ResetDbForTests(db));

            var response = await client.GetAsync("");

            response.AssertOk();

            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var table = content.QuerySelector("table");

            Assert.Null(table);

            var paragraphs = content.QuerySelectorAll("#no-bookings");

            Assert.Single(paragraphs);
        }

        [Fact]
        public async Task ExpectedBookingsTableRowsOnPage_WhenUserHasBooking()
        {
            var startDate = FixedDateTime.UtcNow.AddDays(5);

            var client = _factory.CreateClientWithMemberAndDbSetup(db => 
            {
                DatabaseHelper.ResetDbForTests(db);

                var member = db.Members.Find(1);
                var court = db.Courts.Find(1);

                db.CourtBookings.Add(new CourtBooking
                {
                    Court = court,
                    Member = member,
                    StartDateTime = startDate,
                    EndDateTime = startDate.AddHours(2)
                });

                db.SaveChanges();
            });

            var response = await client.GetAsync("");

            response.AssertOk();

            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var table = content.QuerySelector("table");
            var tableBody = table.QuerySelector("tbody");
            var rows = tableBody.QuerySelectorAll("tr");

            Assert.Single(rows);

            Assert.Collection(rows, r =>
            {
                var cells = r.QuerySelectorAll("td");

                Assert.Equal(startDate.ToString("D"), cells[0].TextContent);
                Assert.Equal("Court 1", cells[1].TextContent);
                Assert.Equal("10 am to 12 pm", cells[2].TextContent);
            });
        }
    }
}

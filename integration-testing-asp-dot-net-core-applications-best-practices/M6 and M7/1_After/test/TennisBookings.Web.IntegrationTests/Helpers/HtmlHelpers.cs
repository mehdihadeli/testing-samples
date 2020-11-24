using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace TennisBookings.Web.IntegrationTests.Helpers
{
    public class HtmlHelpers
    {
        public static async Task<IDocument> GetDocumentAsync(HttpResponseMessage response)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();

            var browser = BrowsingContext.New();

            var document = await browser.OpenAsync(virtualResponse =>
            {
                virtualResponse.Content(contentStream, shouldDispose: true);
                virtualResponse.Address(response.RequestMessage.RequestUri).Status(response.StatusCode);
            });

            return document;
        }
    }
}

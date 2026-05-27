using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class BookingTests : IClassFixture<WebApplicationFactory<WebApp.Program>>
{
    private readonly WebApplicationFactory<WebApp.Program> _factory;
    public BookingTests(WebApplicationFactory<WebApp.Program> factory) => _factory = factory;

    [Fact]
    public async Task BookTable_AsAuthenticatedUser_CreatesBooking()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        // 1) login
        var loginContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("UserNameOrEmail","test@test.com"),
            new KeyValuePair<string,string>("Password","12345")
        });
        var loginResp = await client.PostAsync("/Account/Login", loginContent);
        Assert.Equal(HttpStatusCode.Redirect, loginResp.StatusCode);

        // follow redirect to set cookie
        if (loginResp.Headers.Location != null)
            await client.GetAsync(loginResp.Headers.Location);

        // 2) post booking
        var bookingContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("tableId","T1"),
            new KeyValuePair<string,string>("fromTime", DateTime.UtcNow.AddHours(1).ToString("o")),
            new KeyValuePair<string,string>("toTime", DateTime.UtcNow.AddHours(2).ToString("o"))
        });
        var bookingResp = await client.PostAsync("/Booking/BookTable", bookingContent);
        Assert.Equal(HttpStatusCode.Redirect, bookingResp.StatusCode);

        // (Optionally) verify DB entry by creating a scope and reading AppDbContext
    }
}
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.API;
using VendingMachine.DTO;
using Xunit;

namespace VendingMachine.TEST
{
    public class GuestIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public GuestIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task Get_All_Guests()
        {
            var client = _factory.CreateClient();

            var dto = new List<Guest>
            {
                new Guest { EmailAddress = "fsilangil@gmail.com"},
                new Guest { EmailAddress = "ferdinandsilangil@yahoomail.com"},
            };

            // act            
            var url = "/api/guest/getallguests";
            var response = await client.GetAsync(url);

            // assert
            response.EnsureSuccessStatusCode();
            response.IsSuccessStatusCode.Should().BeTrue();

            string rsp = await response.Content.ReadAsStringAsync();
            var rspAccounts = JsonConvert.DeserializeObject<List<Guest>>(rsp);

            rspAccounts.Should().HaveCount(2);
            rspAccounts[0].EmailAddress.Should().Be("fsilangil@gmail.com");
            rspAccounts[1].EmailAddress.Should().Be("ferdinandsilangil@yahoomail.com");
        }

    }
}

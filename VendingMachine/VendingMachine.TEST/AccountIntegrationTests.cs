using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using VendingMachine.API;
using VendingMachine.DTO;
using Xunit;

namespace VendingMachine.TEST
{
    public class AccountIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AccountIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task Get_All_Accounts()
        {
            var client = _factory.CreateClient();

            var dto = new List<Accounts>
            {
                new Accounts { GuestID = 8, Balance = 500.01},
                new Accounts { GuestID = 1002, Balance = 400.25},
            };

            // act            
            var url = "/api/account/getallaccounts";
            var response = await client.GetAsync(url);

            // assert
            response.EnsureSuccessStatusCode();
            response.IsSuccessStatusCode.Should().BeTrue();

            string rsp = await response.Content.ReadAsStringAsync();
            var rspAccounts = JsonConvert.DeserializeObject<List<Accounts>>(rsp);

            rspAccounts.Should().HaveCount(2);
            rspAccounts[0].GuestID.Should().Be(8);
            rspAccounts[0].Balance.Should().Be(500.01);
            rspAccounts[1].GuestID.Should().Be(1002);
            rspAccounts[1].Balance.Should().Be(400.25);
        }
    }
}

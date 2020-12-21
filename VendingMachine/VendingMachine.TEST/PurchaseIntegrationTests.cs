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
    public class PurchaseIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public PurchaseIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task Get_All_Purchase()
        {
            var client = _factory.CreateClient();

            var dto = new List<Purchase>
            {
                new Purchase { AccountID = 6, ProductID = 1, IsCheckOut = true, Account = new Accounts { GuestID = 8, Balance = 500.01}, 
                               Product = new Products { Name = "Apple Iphone 12", Price = 200, Quantity = 7} },

                new Purchase { AccountID = 6, ProductID = 3, IsCheckOut = true, Account = new Accounts { GuestID = 8, Balance = 500.01},
                               Product = new Products { Name = "Apple Iphone 13", Price = 288, Quantity = 3} },
            };

            // act            
            var url = "/api/purchase/getallpurchase";
            var response = await client.GetAsync(url);

            // assert
            response.EnsureSuccessStatusCode();
            response.IsSuccessStatusCode.Should().BeTrue();

            string rsp = await response.Content.ReadAsStringAsync();
            var rspAccounts = JsonConvert.DeserializeObject<List<Purchase>>(rsp);

            rspAccounts.Should().HaveCount(2);
        }
    }
}

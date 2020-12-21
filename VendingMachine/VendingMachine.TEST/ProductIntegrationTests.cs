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
    public class ProductIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public ProductIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this._factory = factory;
        }

        [Fact]
        public async Task Get_All_Products()
        {
            var client = _factory.CreateClient();

            var dto = new List<Products>
            {
                new Products { Name = "Apple Iphone 12", Price = 200, Quantity = 7},
                new Products { Name = "Apple Iphone 13", Price = 288, Quantity = 3},
            };

            // act            
            var url = "/api/product/getallproducts";
            var response = await client.GetAsync(url);

            // assert
            response.EnsureSuccessStatusCode();
            response.IsSuccessStatusCode.Should().BeTrue();

            string rsp = await response.Content.ReadAsStringAsync();
            var rspAccounts = JsonConvert.DeserializeObject<List<Products>>(rsp);

            rspAccounts.Should().HaveCount(2);
            rspAccounts[0].Name.Should().Be("Apple Iphone 12");
            rspAccounts[0].Price.Should().Be(200);
            rspAccounts[0].Quantity.Should().Be(7);
            rspAccounts[1].Name.Should().Be("Apple Iphone 13");
            rspAccounts[1].Price.Should().Be(288);
            rspAccounts[1].Quantity.Should().Be(3);
        }

    }
}

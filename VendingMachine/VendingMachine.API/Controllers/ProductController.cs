using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DTO;

namespace VendingMachine.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            this._service = service;
        }
        [HttpGet("getallproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await this._service.GetAllProducts();
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._service.GetByID(id);
            return Ok(result);
        }

        [HttpPost("addaproduct")]
        public async Task<IActionResult> AddAProduct([FromBody] Products model)
        {
            var result = await this._service.AddProduct(model);
            return Ok(result);
        }
    }
}

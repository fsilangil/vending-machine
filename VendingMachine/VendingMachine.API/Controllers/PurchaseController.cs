using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DTO;

namespace VendingMachine.API.Controllers
{
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            this._service = service;
        }
        [HttpGet("getallpurchase")]
        public async Task<IActionResult> GetAllPurchase(int accountID)
        {
            var result = await this._service.GetAllPurchase(accountID);
            return Ok(result);
        }
        [HttpGet("getallsummaryorder")]
        public async Task<IActionResult> GetAllSummaryOrder(int accountID)
        {
            var result = await this._service.GetAllSummaryOrder(accountID);
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._service.GetByID(id);
            return Ok(result);
        }

        [HttpPost("addpurchase")]
        public async Task<IActionResult> AddPurchase([FromBody] Purchase model)
        {
            var result = await this._service.AddPurchase(model);
            return Ok(result);
        }

        [HttpPut("checkoutproduct")]
        public async Task<IActionResult> CheckOutProduct([FromBody] List<Purchase> models)
        {
            var result = await this._service.CheckOutItems(models);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DTO;

namespace VendingMachine.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            this._service = service;
        }
        [HttpGet("getallaccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var result = await this._service.GetAllAccounts();
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._service.GetByID(id);
            return Ok(result);
        }
    }
}

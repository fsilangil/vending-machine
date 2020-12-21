using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DTO;
using VendingMachine.DTO.Params;

namespace VendingMachine.API.Controllers
{
    [EnableCors("CorsPolicy")]
    public class GuestController : BaseController
    {
        private readonly IGuestService _service;

        public GuestController(IGuestService service)
        {
            this._service = service;
        }
        [HttpGet("getallguests")]
        public async Task<IActionResult> GetAllGuests()
        {
            var result = await this._service.GetAllGuests();
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._service.GetByID(id);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginParam model)
        {
            var result = await this._service.IsUserExist(model.Email);
            return Ok(result);

        }

        [HttpPost("addguest")]
        public async Task<IActionResult> AddGuest([FromBody] Guest model, double balance)
        {
            var result = await this._service.AddGuest(model, balance);
            return Ok(result);
        }
    }
}

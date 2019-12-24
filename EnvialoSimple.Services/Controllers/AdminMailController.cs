using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.AdminMail;
using .EnvialoSimple.Business.Modules.AdminMail.Models;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace .EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMailController : ControllerBase
    {
        private readonly IAdminMailModule _module;

        public AdminMailController(IAdminMailModule module)
        {
            _module = module;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] FiltroModel filtroModel)
        {
            var operationResult = await _module.GetList(filtroModel);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> CreateAndEdit([FromBody] AdminMailModel model)
        {
            var operationResult = await _module.CreateAndEdit(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("get")]
        public async Task<IActionResult> Get([FromBody] AdminMailModel model)
        {
            var operationResult = await _module.GetItem(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }
    }
}
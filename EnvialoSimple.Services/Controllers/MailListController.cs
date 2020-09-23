using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.MailList;
using EnvialoSimple.Business.Modules.MailList.Models;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailListController : ControllerBase
    {
        private readonly IMailListModule _mailListModule;

        public MailListController(IMailListModule mailListModule)
        {
            _mailListModule = mailListModule;
        }

        [HttpPost("list")]
        public async Task<ActionResult<IList<MailListModel>>> GetList([FromBody] FiltroModel filtroModel)
        {
            var operationResult = await _mailListModule.GetList(filtroModel);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> CreateAndEdit([FromBody] CreateMailListModel model)
        {
            var operationResult = await _mailListModule.CreateAndEdit(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }
    }
}
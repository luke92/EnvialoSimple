using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvialoSimple.Business.Modules.Content;
using EnvialoSimple.Business.Modules.Content.Models;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentModule _contentModule;

        public ContentController(IContentModule contentModule)
        {
            _contentModule = contentModule;
        }

        [HttpPost("edit")]
        public async Task<ActionResult<bool>> CreateAndEdit([FromBody] ContentModel model)
        {
            var operationResult = await _contentModule.SaveContentInCampaign(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }
    }
}
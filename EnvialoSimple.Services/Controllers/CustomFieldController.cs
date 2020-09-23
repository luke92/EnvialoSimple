using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.CustomField;
using EnvialoSimple.Business.Modules.CustomField.Models;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomFieldController : ControllerBase
    {
        private readonly ICustomFieldModule _module;

        public CustomFieldController(ICustomFieldModule module)
        {
            _module = module;
        }

        [HttpPost("list")]
        public async Task<ActionResult<IList<CustomFieldModel>>> GetList([FromBody] FiltroModel filtroModel)
        {
            var operationResult = await _module.GetList(filtroModel);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }
    }
}
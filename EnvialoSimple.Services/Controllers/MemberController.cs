using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.Member;
using .EnvialoSimple.Business.Modules.Member.Models;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace .EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberModule _memberModule;

        public MemberController(IMemberModule memberModule)
        {
            _memberModule = memberModule;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] FiltroModel filtroModel = null)
        {
            var operationResult = await _memberModule.GetList(filtroModel);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> CreateEdit([FromBody] CreateMemberModel model)
        {
            var operationResult = await _memberModule.CreateAndEdit(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

    }
}
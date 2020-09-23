using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.Campaign.Models;
using EnvialoSimple.Business.Modules.Checker;
using EnvialoSimple.Business.Modules.Checker.Models;
using EnvialoSimple.Business.Modules.Member;
using EnvialoSimple.Business.Modules.Member.Models;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberModule _memberModule;
        private readonly ICheckerModule _checkerModule;
        public MemberController(IMemberModule memberModule, ICheckerModule checkerModule)
        {
            _memberModule = memberModule;
            _checkerModule = checkerModule;
        }

        [HttpPost("list")]
        public async Task<ActionResult<IList<MemberModel>>> GetList([FromBody] FiltroModel filtroModel = null)
        {
            var operationResult = await _memberModule.GetList(filtroModel);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<MemberModel>> CreateEdit([FromBody] CreateMemberModel model)
        {
            var operationResult = await _memberModule.CreateAndEdit(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpGet("searchBounceds/{searchBounceds}/{partialNameMailList?}")]
        public async Task<ActionResult<MembersBouncedModel>> SearchBounceds([FromRoute] bool searchBounceds, [FromRoute] string partialNameMailList = "")
        {
            var result = await _checkerModule.GetMemberList(partialNameMailList, searchBounceds);

            if (result.Errors.Count == 0)
                return Ok(result);

            return BadRequest(result);


        }

    }
}
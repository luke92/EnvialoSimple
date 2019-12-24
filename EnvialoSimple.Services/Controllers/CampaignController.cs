using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.Campaign;
using .EnvialoSimple.Business.Modules.Campaign.Models;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace .EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignModule _campaignModule;

        public CampaignController(ICampaignModule campaignModule)
        {
            _campaignModule = campaignModule;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetList([FromBody] FiltroModel filtroModel)
        {
            var operationResult = await _campaignModule.GetList(filtroModel);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> CreateAndEdit([FromBody] CreateCampaignModel model)
        {
            var operationResult = await _campaignModule.CreateAndEdit(model);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("send/{campaingId}")]
        public async Task<IActionResult> Send(string campaingId)
        {
            var operationResult = await _campaignModule.Send(campaingId);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }

        [HttpPost("pause/{campaingId}")]
        public async Task<IActionResult> Pause(string campaingId)
        {
            var operationResult = await _campaignModule.Pause(campaingId);

            if (operationResult.Result != OperationResult.Ok)
                return BadRequest(operationResult);

            return Ok(operationResult);
        }
    }
}
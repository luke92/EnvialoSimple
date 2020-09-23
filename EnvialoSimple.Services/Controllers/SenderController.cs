using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvialoSimple.Business.Modules.Sender;
using EnvialoSimple.Business.Modules.Sender.Models;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvialoSimple.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderModule _senderModule;

        public SenderController(ISenderModule senderModule)
        {
            _senderModule = senderModule;
        }

        [HttpPost]
        public async Task<ActionResult<SenderResponseModel>> CreateEdit([FromBody] SenderRequestModel model)
        {
            var response = await _senderModule.EnviarMail(model);

            if (response.CampaignSended)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
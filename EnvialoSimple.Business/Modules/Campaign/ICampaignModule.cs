using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.Campaign.Models;
using Core.Models;

namespace EnvialoSimple.Business.Modules.Campaign
{
    public interface ICampaignModule
    {
        Task<ResultModel<IList<CampaignModel>>> GetList(FiltroModel filtroModel = null);

        Task<ResultModel<CampaignCreatedModel>> CreateAndEdit(CreateCampaignModel campaignModel);

        Task<ResultModel<bool>> Pause(string campaignId);

        Task<ResultModel<bool>> Send(string campaignId);
    }
}

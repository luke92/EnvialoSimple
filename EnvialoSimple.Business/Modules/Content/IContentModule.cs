using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Modules.Content.Models;
using Core.Models;

namespace EnvialoSimple.Business.Modules.Content
{
    public interface IContentModule
    {
        Task<ResultModel<bool>> SaveContentInCampaign(ContentModel model, string campaignId = null);
    }
}

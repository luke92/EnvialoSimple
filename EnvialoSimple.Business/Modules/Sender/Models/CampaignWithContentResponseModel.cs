using System;
using System.Collections.Generic;
using System.Text;
using .EnvialoSimple.Business.Modules.Campaign.Models;
using .EnvialoSimple.Business.Modules.Content.Models;

namespace .EnvialoSimple.Business.Modules.Sender.Models
{
    public class CampaignWithContentResponseModel
    {
        public CampaignCreatedModel Campaign { get; set; }
        public ContentModel ContentCampaign { get; set; }
        public bool ContentSaved { get; set; }

        public CampaignWithContentResponseModel()
        {
            Campaign = new CampaignCreatedModel();
            ContentCampaign = new ContentModel();
            ContentSaved = false;
        }
    }
}

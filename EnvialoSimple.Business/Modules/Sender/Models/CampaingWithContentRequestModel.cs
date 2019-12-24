using System;
using System.Collections.Generic;
using System.Text;
using .EnvialoSimple.Business.Modules.Campaign.Models;
using .EnvialoSimple.Business.Modules.Content.Models;

namespace .EnvialoSimple.Business.Modules.Sender.Models
{
    public class CampaingWithContentRequestModel
    {
        public CreateCampaignModel Campaign { get; set; }
        public ContentModel Content { get; set; }

        public CampaingWithContentRequestModel()
        {
            Campaign = new CreateCampaignModel();
            Content = new ContentModel();
        }
    }
}

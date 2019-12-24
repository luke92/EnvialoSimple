using System;
using System.Collections.Generic;
using System.Text;

namespace .EnvialoSimple.Business.Modules.Content.Models
{
    public class ContentModel
    {
        public int CampaignID { get; set; }
        public string URL { get; set; }
        public string HTML { get; set; }
        public string PlainText { get; set; }
        public string RemoteUnsubscribeBlock { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace .EnvialoSimple.Business.Modules.Campaign.Models
{
    public class CreateCampaignModel
    {
        public int? CampaignID { get; set; }
        public string CampaignName { get; set; }
        public string CampaignSubject { get; set; }
        public IList<int> MailListsIds { get; set; }
        public int? FromId { get; set; }
        public int? ReplyToId { get; set; }
        public bool TrackLinkClicks { get; set; }
        public bool TrackReads { get; set; }
        public bool TrackAnalitics { get; set; }
        public bool SendStateReport { get; set; }
        public bool ScheduleCampaign { get; set; }
        public DateTime? SendDate { get; set; }
        public bool AddToPublicArchive { get; set; }
        public CreateCampaignModel()
        {
            TrackLinkClicks = false;
            TrackReads = false;
            TrackAnalitics = false;
            SendStateReport = false;
            ScheduleCampaign = false;
            AddToPublicArchive = false;
            MailListsIds = new List<int>();
        }
    }
}

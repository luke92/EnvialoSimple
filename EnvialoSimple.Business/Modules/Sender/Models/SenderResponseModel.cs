using System.Collections.Generic;
using .EnvialoSimple.Business.Modules.AdminMail.Models;
using .EnvialoSimple.Business.Modules.MailList.Models;
using .EnvialoSimple.Business.Modules.Member.Models;

namespace .EnvialoSimple.Business.Modules.Sender.Models
{
    public class SenderResponseModel
    {
        public AdminMailModel From { get; set; }
        public AdminMailModel ReplyTo { get; set; }
        public CreateMailListModel MailList { get; set; }
        public IList<MemberModel> Members { get; set; }
        public CampaignWithContentResponseModel CampaignWithContent { get; set; }
        public bool CampaignSended { get; set; }
        public IList<string> Errors { get; set; }
        public SenderResponseModel()
        {
            From = new AdminMailModel();
            ReplyTo = new AdminMailModel();
            MailList = new CreateMailListModel();
            Members = new List<MemberModel>();
            CampaignWithContent = new CampaignWithContentResponseModel();
            CampaignSended = false;
            Errors = new List<string>();
        }
    }
}

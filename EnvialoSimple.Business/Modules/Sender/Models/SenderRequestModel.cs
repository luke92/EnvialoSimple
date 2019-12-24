using System;
using System.Collections.Generic;
using System.Text;
using .EnvialoSimple.Business.Modules.AdminMail.Models;

namespace .EnvialoSimple.Business.Modules.Sender.Models
{
    public class SenderRequestModel
    {
        public AdminMailModel From { get; set; }
        public AdminMailModel ReplyTo { get; set; }
        public MailListWithMembersModel MailListWithMembers { get; set; }
        public CampaingWithContentRequestModel Campaign { get; set; }

        public SenderRequestModel()
        {
            From = new AdminMailModel();
            ReplyTo = new AdminMailModel();
            MailListWithMembers = new MailListWithMembersModel();
            Campaign = new CampaingWithContentRequestModel();
        }

    }
}

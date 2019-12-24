using System;
using System.Collections.Generic;
using System.Text;
using .EnvialoSimple.Business.Modules.MailList.Models;
using .EnvialoSimple.Business.Modules.Member.Models;

namespace .EnvialoSimple.Business.Modules.Sender.Models
{
    public class MailListWithMembersModel
    {
        public CreateMailListModel MailList { get; set; }
        public IList<CreateMemberModel> Members { get; set; }

        public MailListWithMembersModel()
        {
            MailList = new CreateMailListModel();
            Members = new List<CreateMemberModel>();
        }
    }
}

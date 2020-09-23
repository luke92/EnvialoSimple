using System;
using System.Collections.Generic;
using System.Text;

namespace EnvialoSimple.Business.Modules.MailList.Models
{
    public class MailListModel
    {
        public string MailListID { get; set; }
        public string Name { get; set; }
        public string SourceType { get; set; }
        public string SubscriptionType { get; set; }
        public string MemberCount { get; set; }
        public string ActiveMemberCount { get; set; }
        public string Deleted { get; set; }
        public string Status { get; set; }
    }
}

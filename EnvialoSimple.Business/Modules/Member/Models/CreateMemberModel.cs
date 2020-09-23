using System;
using System.Collections.Generic;
using System.Text;

namespace EnvialoSimple.Business.Modules.Member.Models
{
    public class CreateMemberModel
    {
        public string MemberID { get; set; }
        public string MailListID { get; set; }
        public string Email { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }

        public List<string> CustomFields { get; set; }

        public CreateMemberModel()
        {
            CustomFields = new List<string>();
        }
    }
}

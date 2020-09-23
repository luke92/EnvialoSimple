using System;
using System.Collections.Generic;
using System.Text;
using EnvialoSimple.Business.Modules.MailList.Models;
using EnvialoSimple.Business.Modules.Member.Models;

namespace EnvialoSimple.Business.Modules.Checker.Models
{
    public class MembersBouncedModel
    {
        public IList<string> Errors { get; set; }

        public int Count { get; set; }

        public bool AreBounceds { get; set; }

        public ISet<MemberReducedModel> Members { get; set; }

        public MembersBouncedModel()
        {
            Errors = new List<string>();
            Members = new HashSet<MemberReducedModel>();
            Count = 0;
            AreBounceds = false;
        }
    }
}

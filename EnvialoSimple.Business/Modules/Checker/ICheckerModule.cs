using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.Checker.Models;
using Core.Models;

namespace EnvialoSimple.Business.Modules.Checker
{
    public interface ICheckerModule
    {
        Task<MembersBouncedModel> GetMemberList(string partialNameMailList, bool searchBounceds);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.Member.Models;
using Models;

namespace .EnvialoSimple.Business.Modules.Member
{
    public interface IMemberModule
    {
        Task<ResultModel<IList<MemberModel>>> GetList(FiltroModel filtroModel = null);
        Task<ResultModel<MemberModel>> CreateAndEdit(CreateMemberModel model, string mailListId = null);
        Task<ResultModel<bool>> CreateAndEditMembers(IList<CreateMemberModel> membersModels, string mailListId = null);
    }
}

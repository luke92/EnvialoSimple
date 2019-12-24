using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.AdminMail.Models;
using Models;

namespace .EnvialoSimple.Business.Modules.AdminMail
{
    public interface IAdminMailModule
    {
        Task<ResultModel<IList<AdminMailModel>>> GetList(FiltroModel filtroModel = null);

        Task<ResultModel<AdminMailModel>> CreateAndEdit(AdminMailModel adminMailModel);

        Task<ResultModel<AdminMailModel>> GetItem(AdminMailModel adminMailModel);
    }
}

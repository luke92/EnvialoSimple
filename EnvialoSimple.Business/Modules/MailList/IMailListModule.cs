using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.MailList.Models;
using Models;

namespace .EnvialoSimple.Business.Modules.MailList
{
    public interface IMailListModule
    {
        Task<ResultModel<IList<MailListModel>>> GetList(FiltroModel filtroModel = null);
        Task<ResultModel<CreateMailListModel>> CreateAndEdit(CreateMailListModel model);
    }
}

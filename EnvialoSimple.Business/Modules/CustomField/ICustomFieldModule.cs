using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.CustomField.Models;
using Core.Models;

namespace EnvialoSimple.Business.Modules.CustomField
{
    public interface ICustomFieldModule
    {
        Task<ResultModel<IList<CustomFieldModel>>> GetList(FiltroModel filtroModel = null);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Modules.Sender.Models;
using Core.Models;

namespace EnvialoSimple.Business.Modules.Sender
{
    public interface ISenderModule
    {
        Task<SenderResponseModel> EnviarMail(SenderRequestModel model);
    }
}

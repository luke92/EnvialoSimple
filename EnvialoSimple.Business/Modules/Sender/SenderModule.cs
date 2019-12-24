using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.AdminMail;
using .EnvialoSimple.Business.Modules.Campaign;
using .EnvialoSimple.Business.Modules.Content;
using .EnvialoSimple.Business.Modules.MailList;
using .EnvialoSimple.Business.Modules.Member;
using .EnvialoSimple.Business.Modules.Member.Models;
using .EnvialoSimple.Business.Modules.Sender.Models;
using Models;

namespace .EnvialoSimple.Business.Modules.Sender
{
    public class SenderModule : ISenderModule
    {
        private readonly IAdminMailModule _adminMailModule;
        private readonly IMailListModule _mailListModule;
        private readonly IMemberModule _memberModule;
        private readonly ICampaignModule _campaignModule;
        private readonly IContentModule _contentModule;
        private readonly BaseURI _baseUri;

        public SenderModule(BaseURI baseUri, IAdminMailModule adminMailModule, IMailListModule mailListModule,
            IMemberModule memberModule, ICampaignModule campaignModule, IContentModule contentModule)
        {
            _adminMailModule = adminMailModule;
            _mailListModule = mailListModule;
            _memberModule = memberModule;
            _campaignModule = campaignModule;
            _contentModule = contentModule;
            _baseUri = baseUri;
        }
        public async Task<SenderResponseModel> EnviarMail(SenderRequestModel model)
        {
            var response = new SenderResponseModel();
            try
            {
                if (string.IsNullOrEmpty(model.From.EmailID) && string.IsNullOrEmpty(model.From.EmailAddress))
                {
                    model.From.EmailAddress = _baseUri.GetFromEmailDefault();
                }
                var from = await _adminMailModule.GetItem(model.From);
                response.From = from.Data;
                response.Errors = GetErrors(from.Errors);
                if (from.Result != OperationResult.Ok)
                {
                    return response;
                }

                if (string.IsNullOrEmpty(model.ReplyTo.EmailID) && string.IsNullOrEmpty(model.ReplyTo.EmailAddress))
                {
                    model.ReplyTo.EmailAddress = _baseUri.GetReplyToDefault();
                }
                var reply = await _adminMailModule.GetItem(model.ReplyTo);
                response.ReplyTo = reply.Data;
                response.Errors = GetErrors(reply.Errors);
                if (reply.Result != OperationResult.Ok)
                {
                    return response;
                }

                if (string.IsNullOrEmpty(model.MailListWithMembers.MailList.MailListName) &&
                    string.IsNullOrEmpty(model.MailListWithMembers.MailList.MailListID))
                {
                    model.MailListWithMembers.MailList.MailListName = "MailList" + DateTime.Now.ToString("s");
                }
                var mailList = await _mailListModule.CreateAndEdit(model.MailListWithMembers.MailList);
                response.MailList = mailList.Data;
                response.Errors = GetErrors(mailList.Errors);
                if (mailList.Result != OperationResult.Ok)
                {
                    return response;
                }


                model.Campaign.Campaign.FromId = int.Parse(from.Data.EmailID);
                model.Campaign.Campaign.ReplyToId = int.Parse(reply.Data.EmailID);
                model.Campaign.Campaign.MailListsIds.Add(int.Parse(mailList.Data.MailListID));

                if (string.IsNullOrEmpty(model.Campaign.Campaign.CampaignSubject))
                {
                    model.Campaign.Campaign.CampaignSubject = _baseUri.GetSubjectDefault();
                }

                if (string.IsNullOrEmpty(model.Campaign.Campaign.CampaignName))
                {
                    model.Campaign.Campaign.CampaignName = "Campaign" + DateTime.Now.ToString("s");
                }
                var campaign = await _campaignModule.CreateAndEdit(model.Campaign.Campaign);
                response.CampaignWithContent.Campaign = campaign.Data;
                response.Errors = GetErrors(campaign.Errors);
                if (campaign.Result != OperationResult.Ok)
                {
                    return response;
                }

                var campaignContent = await _contentModule.SaveContentInCampaign(model.Campaign.Content, campaign.Data.CampaignId);
                response.CampaignWithContent.ContentCampaign = model.Campaign.Content;
                response.Errors = GetErrors(campaignContent.Errors);
                if (campaignContent.Result != OperationResult.Ok)
                {
                    return response;
                }
                
                CreateMembers(model.MailListWithMembers.Members, mailList.Data.MailListID);

                var sendCampaing = await _campaignModule.Send(campaign.Data.CampaignId);
                response.CampaignSended = sendCampaing.Data;
                response.Errors = GetErrors(sendCampaing.Errors);
                if (sendCampaing.Result != OperationResult.Ok)
                {
                    return response;
                }

                response.CampaignSended = true;

            }
            catch (Exception e)
            {
                response.Errors.Add(e.Message);
            }

            return response;

        }

        private async void CreateMembers(IList<CreateMemberModel> members, string mailListId)
        {
            await Task.Run(async () =>
            {
                await _memberModule.CreateAndEditMembers(members, mailListId);
            });
        }

        private IList<string> GetErrors(IEnumerable<ErrorModel> errors)
        {
            var response = new List<string>();

            if (errors != null )
            {
                foreach (var error in errors)
                {
                    response.Add(error.Message);
                }
            }

            return response;
        }

    }
}

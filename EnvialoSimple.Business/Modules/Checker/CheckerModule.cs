using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.Checker.Models;
using EnvialoSimple.Business.Modules.MailList;
using EnvialoSimple.Business.Modules.MailList.Models;
using EnvialoSimple.Business.Modules.Member;
using EnvialoSimple.Business.Modules.Member.Models;
using Core.Models;

namespace EnvialoSimple.Business.Modules.Checker
{
    public class CheckerModule : ICheckerModule
    {
        private readonly IMailListModule _mailListModule;
        private readonly IMemberModule _memberModule;
        private readonly int countDefault = 1000;

        public CheckerModule(IMailListModule mailListModule, IMemberModule memberModule)
        {
            _memberModule = memberModule;
            _mailListModule = mailListModule;
        }

        public async Task<MembersBouncedModel> GetMemberList(string partialNameMailList, bool searchBounceds)
        {
            var membersBounceds = new MembersBouncedModel();
            membersBounceds.AreBounceds = searchBounceds;
            try
            {
                var memberList = new List<MemberModel>();

                var maillists = await GetMailLists(partialNameMailList);

                membersBounceds.Errors = GetErrors(maillists.Errors);

                if (maillists.Result != OperationResult.Ok)
                {
                    return membersBounceds;
                }

                var membersInMailLists = await GetMembersInMailLists(maillists.Data);

                membersBounceds.Errors = GetErrors(membersInMailLists.Errors);

                if (membersInMailLists.Result != OperationResult.Ok)
                {
                    return membersBounceds;
                }

                memberList = GetMembersWithFilterBounced(membersInMailLists.Data, searchBounceds);

                foreach (var member in memberList)
                {
                    if (!membersBounceds.Members.Contains(new MemberReducedModel(member)))
                        membersBounceds.Members.Add(new MemberReducedModel(member));
                }

                membersBounceds.Count = membersBounceds.Members.Count;

            }

            catch (Exception e)
            {
                membersBounceds.Errors.Add(e.Source + " " + e.Message);
            }

            return membersBounceds;
        }


        private List<MemberModel> GetMembersWithFilterBounced(IList<MemberModel> members, bool searchBounceds)
        {
            var memberList = new HashSet<MemberModel>();

            foreach (var member in members)
            {
                // list.item.BounceType Tipo de rebote[1 = Rebote Duro | 2 = Rebote Blando | 3 = Sin Rebotes]
                if (searchBounceds)
                {
                    if (member.BounceType == "1" || member.BounceType == "2")
                    {
                        if (!memberList.Contains(member))
                            memberList.Add(member);
                    }
                }
                else
                {
                    if (member.BounceType == "3")
                    {
                        if (!memberList.Contains(member))
                            memberList.Add(member);
                    }
                }
            }

            return memberList.ToList();
        }

        private async Task<ResultModel<IList<MemberModel>>> GetMembersInMailLists(IList<MailListModel> mailLists)
        {
            var mailListsIds = new List<int>();
            var list = new List<MemberModel>();

            foreach (var mailList in mailLists)
            {
                if (int.TryParse(mailList.MailListID, out int mailListId))
                {
                    mailListsIds.Add(mailListId);
                }
            }

            int absolutePage = 1;
            var continueSearch = true;
            while (continueSearch)
            {
                var filtroModel = new FiltroModel(mailListsIds, countDefault, absolutePage, null, "", null, null);
                var memberLists = await _memberModule.GetList(filtroModel);
                if (memberLists.Data.Count == 0)
                {
                    continueSearch = false;
                }
                else
                {
                    list.AddRange(memberLists.Data);
                }
                absolutePage++;
            }

            return new SuccessResultModel<IList<MemberModel>>(list);
        }

        private async Task<ResultModel<IList<MailListModel>>> GetMailLists(string partialName)
        {
            if (partialName != null)
                partialName.Trim();

            var filtroModel = new FiltroModel(new List<int>(), countDefault, null, null, partialName, null, null);

            var mailLists = await _mailListModule.GetList(filtroModel);

            return mailLists;

        }

        private IList<string> GetErrors(IEnumerable<ErrorModel> errors)
        {
            var response = new List<string>();

            if (errors != null)
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

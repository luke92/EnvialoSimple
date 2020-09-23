using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.Member.Models;
using Core.Models;
using Newtonsoft.Json.Linq;

namespace EnvialoSimple.Business.Modules.Member
{
    public class MemberModule : IMemberModule
    {
        private readonly string _modulo = "member";
        private readonly BaseURI _baseUri;

        public MemberModule(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<ResultModel<IList<MemberModel>>> GetList(FiltroModel filtroModel = null)
        {
            var action = "list";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());
                    if (filtroModel != null)
                        url += String.Format("&{0}", filtroModel.GetSearchQuery());
                    HttpResponseMessage response =
                        await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    try
                    {
                        JArray items = (JArray)rss["root"]["ajaxResponse"]["list"]["item"];


                        IList<MemberModel> models = new List<MemberModel>();
                        foreach (var item in items.Children())
                        {
                            var model = item.ToObject<MemberModel>();
                            models.Add(model);
                        }

                        return new SuccessResultModel<IList<MemberModel>>(models);
                    }
                    catch
                    {
                        JToken item = rss["root"]["ajaxResponse"]["errors"];
                        return new ErrorResultModel<IList<MemberModel>>(item.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<IList<MemberModel>>(e.Message);
            }
        }

        public async Task<ResultModel<MemberModel>> CreateAndEdit(CreateMemberModel model, string mailListId = null)
        {
            var action = "edit";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();

                    if (!string.IsNullOrEmpty(model.MemberID))
                    {
                        parameters.Add(new KeyValuePair<string, string>("MemberID", model.MemberID));
                    }

                    if (mailListId != null && !string.IsNullOrEmpty(mailListId.Trim()))
                    {
                        parameters.Add(new KeyValuePair<string, string>("MailListID", mailListId));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.MailListID))
                        {
                            parameters.Add(new KeyValuePair<string, string>("MailListID", model.MailListID));
                        }
                    }

                    parameters.Add(new KeyValuePair<string, string>("Email", model.Email));

                    if (model.CustomFields != null && model.CustomFields.Count < 3)
                    {
                        parameters.Add(new KeyValuePair<string, string>("CustomField1", model.CustomField1));
                        parameters.Add(new KeyValuePair<string, string>("CustomField2", model.CustomField2));
                    }
                    else
                    {
                        for (int i = 0; i < model.CustomFields.Count; i++)
                        {
                            parameters.Add(new KeyValuePair<string, string>("CustomField" + (i + 1), model.CustomFields[i]));
                        }
                    }


                    var formContent = new FormUrlEncodedContent(parameters);

                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());

                    HttpResponseMessage response =
                        await client.PostAsync(url, formContent);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    try
                    {
                        JToken item = rss["root"]["ajaxResponse"]["member"];


                        MemberModel member = new MemberModel();
                        member = item.ToObject<MemberModel>();

                        return new SuccessResultModel<MemberModel>(member);
                    }
                    catch
                    {
                        JToken item = rss["root"]["ajaxResponse"]["errors"];
                        return new ErrorResultModel<MemberModel>(item.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<MemberModel>(e.Message);
            }
        }

        public async Task<ResultModel<bool>> CreateAndEditMembers(IList<CreateMemberModel> membersModels, string mailListId = null)
        {
            try
            {

                foreach (var memberModel in membersModels)
                {
                    await Task.Run(async () =>
                    {
                        await CreateAndEdit(memberModel, mailListId);
                    });
                }

                return new SuccessResultModel<bool>(true);

            }
            catch (Exception e)
            {
                return new ErrorResultModel<bool>(e.Message);
            }

        }
    }
}

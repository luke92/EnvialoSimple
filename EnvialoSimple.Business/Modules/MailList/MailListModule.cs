using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.MailList.Models;
using Core.Models;
using Newtonsoft.Json.Linq;

namespace EnvialoSimple.Business.Modules.MailList
{
    public class MailListModule : IMailListModule
    {
        private readonly string _modulo = "maillist";
        private readonly BaseURI _baseUri;

        public MailListModule(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<ResultModel<IList<MailListModel>>> GetList(FiltroModel filtroModel = null)
        {
            var action = "list";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());
                    if (filtroModel != null)
                        url += String.Format("&{0}", filtroModel.GetSearchQuery("MailListsIds"));
                    HttpResponseMessage response =
                        await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    try
                    {
                        JArray items = (JArray)rss["root"]["ajaxResponse"]["list"]["item"];

                        IList<MailListModel> models = new List<MailListModel>();
                        if (items != null)
                        {
                            foreach (var item in items.Children())
                            {
                                models.Add(item.ToObject<MailListModel>());
                            }
                            return new SuccessResultModel<IList<MailListModel>>(models);
                        }
                        else
                        {
                            return new ErrorResultModel<IList<MailListModel>>(
                                "No se encontraron Listas de mails que coincidan con la búsqueda realizada");
                        }

                    }
                    catch
                    {
                        JToken item = rss["root"]["ajaxResponse"]["errors"];
                        return new ErrorResultModel<IList<MailListModel>>(item.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<IList<MailListModel>>(e.Message);
            }
        }

        public async Task<ResultModel<CreateMailListModel>> CreateAndEdit(CreateMailListModel model)
        {
            var action = "edit";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();

                    if (!string.IsNullOrEmpty(model.MailListID))
                    {
                        parameters.Add(new KeyValuePair<string, string>("MailListID", model.MailListID));
                    }

                    if (!string.IsNullOrEmpty(model.MailListName))
                    {
                        parameters.Add(new KeyValuePair<string, string>("MailListName", model.MailListName));
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
                        JToken item = rss["root"]["ajaxResponse"]["maillist"];


                        CreateMailListModel mailList = new CreateMailListModel();
                        mailList = item.ToObject<CreateMailListModel>();

                        return new SuccessResultModel<CreateMailListModel>(mailList);
                    }
                    catch
                    {
                        JToken item = rss["root"]["ajaxResponse"]["errors"];
                        return new ErrorResultModel<CreateMailListModel>(item.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<CreateMailListModel>(e.Message);
            }
        }
    }
}

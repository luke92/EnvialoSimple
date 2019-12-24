using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.AdminMail.Models;
using Models;
using Newtonsoft.Json.Linq;

namespace .EnvialoSimple.Business.Modules.AdminMail
{
    public class AdminMailModule : IAdminMailModule
    {
        private readonly string _modulo = "administratoremail";
        private readonly BaseURI _baseUri;

        public AdminMailModule(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<ResultModel<IList<AdminMailModel>>> GetList(FiltroModel filtroModel = null)
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

                    JArray items = (JArray)rss["root"]["ajaxResponse"]["list"]["item"];


                    IList<AdminMailModel> models = new List<AdminMailModel>();
                    foreach (var item in items.Children())
                    {
                        models.Add(item.ToObject<AdminMailModel>());
                    }

                    return new SuccessResultModel<IList<AdminMailModel>>(models);
                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<IList<AdminMailModel>>(e.Message);
            }
        }

        public async Task<ResultModel<AdminMailModel>> CreateAndEdit(AdminMailModel adminMailModel)
        {
            var action = "edit";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();

                    if (!string.IsNullOrEmpty(adminMailModel.EmailID))
                    {
                        parameters.Add(new KeyValuePair<string, string>("EmailID", adminMailModel.EmailID));
                    }

                    if (!string.IsNullOrEmpty(adminMailModel.Name))
                    {
                        parameters.Add(new KeyValuePair<string, string>("Name", adminMailModel.Name));
                    }

                    if (!string.IsNullOrEmpty(adminMailModel.Name))
                    {
                        parameters.Add(new KeyValuePair<string, string>("EmailAddress", adminMailModel.EmailAddress));
                    }

                    var formContent = new FormUrlEncodedContent(parameters);

                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());

                    HttpResponseMessage response =
                        await client.PostAsync(url, formContent);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    JToken item = rss["root"]["ajaxResponse"]["email"];


                    AdminMailModel campaingCreatedModel = new AdminMailModel();
                    campaingCreatedModel = item.ToObject<AdminMailModel>();

                    return new SuccessResultModel<AdminMailModel>(campaingCreatedModel);
                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<AdminMailModel>(e.Message);
            }
        }

        public async Task<ResultModel<AdminMailModel>> GetItem(AdminMailModel adminMailModel)
        {
            
                
            try
            {
                var listaAdminsMail = await GetList();
                foreach (var adminMail in listaAdminsMail.Data)
                {
                    if (!string.IsNullOrEmpty(adminMailModel.EmailID))
                    {
                        if (adminMail.EmailID.ToUpper().Equals(adminMailModel.EmailID.ToUpper()))
                            return new SuccessResultModel<AdminMailModel>(adminMail);
                    }

                    if (adminMail.EmailAddress.ToUpper().Equals(adminMailModel.EmailAddress.ToUpper()))
                        return new SuccessResultModel<AdminMailModel>(adminMail);
                }

                return new ErrorResultModel<AdminMailModel>("No se encontró mail registrado");
            }
            catch (Exception e)
            {
                return new ErrorResultModel<AdminMailModel>(e.Message);
            }
        }
    }
}

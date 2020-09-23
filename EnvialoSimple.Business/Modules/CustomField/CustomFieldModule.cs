using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.CustomField.Models;
using Core.Models;
using Newtonsoft.Json.Linq;

namespace EnvialoSimple.Business.Modules.CustomField
{
    public class CustomFieldModule : ICustomFieldModule
    {
        private readonly string _modulo = "customfield";
        private readonly BaseURI _baseUri;

        public CustomFieldModule(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }
        public async Task<ResultModel<IList<CustomFieldModel>>> GetList(FiltroModel filtroModel = null)
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


                        IList<CustomFieldModel> models = new List<CustomFieldModel>();
                        foreach (var item in items.Children())
                        {
                            models.Add(item.ToObject<CustomFieldModel>());
                        }

                        return new SuccessResultModel<IList<CustomFieldModel>>(models);
                    }
                    catch
                    {
                        JToken item = rss["root"]["ajaxResponse"]["errors"];
                        return new ErrorResultModel<IList<CustomFieldModel>>(item.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<IList<CustomFieldModel>>(e.Message);
            }
        }
    }
}

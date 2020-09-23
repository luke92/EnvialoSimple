using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EnvialoSimple.Business.Helpers;
using EnvialoSimple.Business.Modules.Content.Models;
using Core.Models;
using Newtonsoft.Json.Linq;

namespace EnvialoSimple.Business.Modules.Content
{
    public class ContentModule : IContentModule
    {
        private readonly string _modulo = "content";
        private readonly BaseURI _baseUri;

        public ContentModule(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }
        public async Task<ResultModel<bool>> SaveContentInCampaign(ContentModel model, string campaignId = null)
        {
            var action = "edit";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();

                    if (!string.IsNullOrEmpty(campaignId.Trim()))
                    {
                        parameters.Add(new KeyValuePair<string, string>("CampaignID", campaignId));
                    }
                    else
                    {
                        parameters.Add(new KeyValuePair<string, string>("CampaignID", model.CampaignID.ToString()));
                    }

                    if (!string.IsNullOrEmpty(model.HTML))
                    {
                        parameters.Add(new KeyValuePair<string, string>("HTML", model.HTML));
                    }

                    if (!string.IsNullOrEmpty(model.PlainText))
                    {
                        parameters.Add(new KeyValuePair<string, string>("PlainText", model.PlainText));
                    }

                    if (!string.IsNullOrEmpty(model.RemoteUnsubscribeBlock))
                    {
                        parameters.Add(new KeyValuePair<string, string>("RemoteUnsubscribeBlock", model.RemoteUnsubscribeBlock));
                    }

                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());

                    var content = new MultipartFormDataContent();
                    foreach (var parameter in parameters)
                    {
                        content.Add(new StringContent(parameter.Value), parameter.Key);
                    }

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    try
                    {
                        JToken item = rss["root"]["ajaxResponse"]["success"];

                        var responseObject = item.ToObject<int>();

                        var contentCreated = responseObject > 0;
                        return new SuccessResultModel<bool>(contentCreated);
                    }
                    catch
                    {
                        JToken item = rss["root"]["ajaxResponse"]["errors"];
                        return new ErrorResultModel<bool>(item.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<bool>(e.Message);
            }
        }
    }
}

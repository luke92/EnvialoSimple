using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using .EnvialoSimple.Business.Helpers;
using .EnvialoSimple.Business.Modules.Campaign.Models;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace .EnvialoSimple.Business.Modules.Campaign
{
    public class CampaignModule : ICampaignModule
    {
        private readonly string _modulo = "campaign";
        private readonly BaseURI _baseUri;

        public CampaignModule(BaseURI baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<ResultModel<IList<CampaignModel>>> GetList(FiltroModel filtroModel = null)
        {
            var action = "list";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());
                    if(filtroModel != null)
                        url += String.Format("&{0}",filtroModel.GetSearchQuery("MailListsIds"));
                    HttpResponseMessage response =
                        await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    JArray items = (JArray)rss["root"]["ajaxResponse"]["list"]["item"];

                    
                    IList<CampaignModel> campaingsModels = new List<CampaignModel>();
                    foreach (var item in items.Children())
                    {
                        campaingsModels.Add(item.ToObject<CampaignModel>());
                    }

                    return new SuccessResultModel<IList<CampaignModel>>(campaingsModels);
                }
            }
            catch(Exception e)
            {
                return new ErrorResultModel<IList<CampaignModel>>(e.Message);
            }
        }

        public async Task<ResultModel<CampaignCreatedModel>> CreateAndEdit(CreateCampaignModel campaignModel)
        {
            var action = "edit";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();

                    if (campaignModel.CampaignID.HasValue)
                    {
                        parameters.Add(new KeyValuePair<string, string>("CampaignID", campaignModel.CampaignID.Value.ToString()));
                    }

                    if (!string.IsNullOrEmpty(campaignModel.CampaignName))
                    {
                        parameters.Add(new KeyValuePair<string, string>("CampaignName", campaignModel.CampaignName));
                    }

                    if (!string.IsNullOrEmpty(campaignModel.CampaignSubject))
                    {
                        parameters.Add(new KeyValuePair<string, string>("CampaignSubject", campaignModel.CampaignSubject));
                    }

                    foreach (var mailList in campaignModel.MailListsIds)
                    {
                        parameters.Add(new KeyValuePair<string, string>("MailListsIds[]", mailList.ToString()));
                    }

                    if (campaignModel.FromId.HasValue)
                    {
                        parameters.Add(new KeyValuePair<string, string>("FromID", campaignModel.FromId.Value.ToString()));
                    }

                    if (campaignModel.ReplyToId.HasValue)
                    {
                        parameters.Add(new KeyValuePair<string, string>("ReplyToID", campaignModel.ReplyToId.Value.ToString()));
                    }
                    
                    parameters.Add(new KeyValuePair<string, string>("TrackLinkClicks", Convert.ToInt32(campaignModel.TrackLinkClicks).ToString()));

                    parameters.Add(new KeyValuePair<string, string>("TrackReads", Convert.ToInt32(campaignModel.TrackReads).ToString()));

                    parameters.Add(new KeyValuePair<string, string>("TrackAnalitics", Convert.ToInt32(campaignModel.TrackAnalitics).ToString()));

                    parameters.Add(new KeyValuePair<string, string>("SendStateReport", Convert.ToInt32(campaignModel.SendStateReport).ToString()));

                    parameters.Add(new KeyValuePair<string, string>("ScheduleCampaign", Convert.ToInt32(campaignModel.ScheduleCampaign).ToString()));

                    if (campaignModel.SendDate.HasValue)
                    {
                        parameters.Add(new KeyValuePair<string, string>("SendDate", campaignModel.SendDate.Value.ToString("yyyy-MM-dd HH:mm:ss")));
                    }
                    
                    var formContent = new FormUrlEncodedContent(parameters);

                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());

                    HttpResponseMessage response =
                        await client.PostAsync(url, formContent);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    JToken item = rss["root"]["ajaxResponse"]["campaign"];


                    CampaignCreatedModel campaingCreatedModel = new CampaignCreatedModel();
                    campaingCreatedModel = item.ToObject<CampaignCreatedModel>();

                    return new SuccessResultModel<CampaignCreatedModel>(campaingCreatedModel);
                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<CampaignCreatedModel>(e.Message);
            }
        }

        public async Task<ResultModel<bool>> Pause(string campaignId)
        {
            var action = "pause";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();

                    parameters.Add(new KeyValuePair<string, string>("CampaignID", campaignId));

                    var content = new FormUrlEncodedContent(parameters);

                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    JToken item = rss["root"]["ajaxResponse"]["success"];

                    var responseObject = item.ToObject<int>();

                    var contentCreated = responseObject > 0;
                    return new SuccessResultModel<bool>(contentCreated);
                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<bool>(e.Message);
            }
        }

        public async Task<ResultModel<bool>> Send(string campaignId)
        {
            var action = "resume";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var parameters = new List<KeyValuePair<string, string>>();
                    
                    parameters.Add(new KeyValuePair<string, string>("CampaignID", campaignId));
                    
                    var content = new FormUrlEncodedContent(parameters);

                    var url = String.Format("{0}/{1}/{2}?{3}&{4}", _baseUri.GetURI(), _modulo, action,
                        _baseUri.GetAPIKEY(), _baseUri.GetFormat());

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();

                    JObject rss = JObject.Parse(json);

                    JToken item = rss["root"]["ajaxResponse"]["success"];

                    var responseObject = item.ToObject<int>();

                    var contentCreated = responseObject > 0;
                    return new SuccessResultModel<bool>(contentCreated);
                }
            }
            catch (Exception e)
            {
                return new ErrorResultModel<bool>(e.Message);
            }
        }
    }
}

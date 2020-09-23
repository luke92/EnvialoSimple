using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EnvialoSimple.Business.Modules.Campaign.Models
{
    public class CampaignModel
    {
        public string CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string CampaignType { get; set; }

        public string RelFromEmailId { get; set; }

        public string FromEmailIdType { get; set; }

        public string RelReplyToEmailId { get; set; }

        public string ReplyToEmailIdType { get; set; }

        public string RelReturnPathEmailId { get; set; }

        public string ReturnPathEmailIdType { get; set; }

        public string SendReportToAdmin { get; set; }

        public string CampaignStatisticsId { get; set; }

        public string TotalRecipients { get; set; }

        public string SentRecipients { get; set; }

        public string FailedRecipients { get; set; }

        public string SendStartDateTime { get; set; }

        public long TotalComplaints { get; set; }

        public long TotalSoftBounces { get; set; }

        public long TotalHardBounces { get; set; }

        public long TotalUnsubscriptions { get; set; }

        public long TotalBounces { get; set; }

        public string SendStartTimestamp { get; set; }

        public string SendFinishDateTime { get; set; }

        public string SendFinishTimestamp { get; set; }

        public long ReadAmount { get; set; }

        public decimal ReadAmountPercentage { get; set; }

        public long UniqueReadAmount { get; set; }

        public decimal UniqueReadAmountPercentage { get; set; }

        public long ClickAmount { get; set; }

        public string Subject { get; set; }

        public string SendStateReport { get; set; }

        public string AddToPublicArchive { get; set; }

        public string TrackLinkClicks { get; set; }

        public string TrackReads { get; set; }

        public string TrackAnalitics { get; set; }

        public string SpamPoints { get; set; }

        public List<SpamDetail> SpamDetail { get; set; }

        public string SendStartDateTimeFormated { get; set; }

        public string SendFinishDateTimeFormated { get; set; }

        public List<object> ScheduleErrorStatus { get; set; }

        public string ScheduleSendDate { get; set; }

        public string ScheduleSendDateFormated { get; set; }

        public string Status { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal BouncesPercentage { get; set; }

        public long Sent { get; set; }

        public long ComplaintLimit { get; set; }

        public Integrity IntegrityDetail { get; set; }

        public long SentRoles { get; set; }

        public string EditorVersion { get; set; }

        public string Workspace { get; set; }

        public CampaignModel()
        {
            ScheduleErrorStatus = new List<object>();
        }
        
    }
    public class SpamDetail
    {
        public string Pts { get; set; }

        public string Rule { get; set; }

        public string Desc { get; set; }
    }
}

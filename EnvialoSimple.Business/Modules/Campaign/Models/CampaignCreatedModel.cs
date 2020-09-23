using System;
using System.Collections.Generic;
using System.Text;
using EnvialoSimple.Business.Modules.MailList.Models;
using Newtonsoft.Json;

namespace EnvialoSimple.Business.Modules.Campaign.Models
{
    public class CampaignCreatedModel
    {
        public string CampaignId { get; set; }

        public string Name { get; set; }

        public From From { get; set; }

        public From ReplyTo { get; set; }

        public long SendStateReport { get; set; }

        public string Status { get; set; }

        public long AddToPublicArchive { get; set; }

        public string TotalRecipients { get; set; }

        public string Type { get; set; }

        public Statistics Statistics { get; set; }

        public string ThumbnailUrl { get; set; }

        public Schedule Schedule { get; set; }

        public string Subject { get; set; }

        public long TrackLinkClicks { get; set; }

        public long TrackReads { get; set; }

        public long TrackAnalitics { get; set; }

        public string Workspace { get; set; }

        public long Content { get; set; }

        public SocialShare SocialShare { get; set; }

        public Integrity Integrity { get; set; }

        public string ResendCampaign { get; set; }

        public string CustomTo { get; set; }

        public Maillists Maillists { get; set; }

        public Segments Segments { get; set; }

        public long IsEditable { get; set; }

        [JsonProperty("editorVersion")]
        public string EditorVersion { get; set; }

        public CampaignCreatedModel()
        {
            From = new From();
            Integrity = new Integrity();
            Maillists = new Maillists();
            ReplyTo = new From();
            Schedule = new Schedule();
            Segments = new Segments();
            SocialShare = new SocialShare();
            Statistics = new Statistics();
        }
    }

    public class Segments
    {
        public List<SegmentsRow> Rows { get; set; }

        public long Count { get; set; }

        public long TotalCount { get; set; }
    }

    public class SegmentsRow
    {
        public string SegmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MemberCount { get; set; }

        public List<object> Rules { get; set; }

        public List<long> MaillistsIds { get; set; }

        public List<long> CampaingsIds { get; set; }

        public string RelBatchJobId { get; set; }

        public BatchJob BatchJob { get; set; }
    }

    public class BatchJob
    {
        public string BatchJobId { get; set; }

        public string UserId { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string Progress { get; set; }

        public Params Params { get; set; }

        public Response Response { get; set; }

        public string ErrorCode { get; set; }

        public string Visible { get; set; }
    }

    public class Response
    {
        public long EnlapsedTime { get; set; }

        public long Total { get; set; }
    }

    public class Params
    {
        public string SegmentId { get; set; }

        public List<long> MailListsIds { get; set; }

        public List<long> CampaignsIds { get; set; }

        public List<object> Filter { get; set; }
    }

    public class From
    {
        public string EmailId { get; set; }

        public string EmailAddress { get; set; }

        public string Name { get; set; }
    }

    public class Integrity
    {
        public long Status { get; set; }

        public IntegrityStatus Schedule { get; set; }

        public IntegrityStatus Subject { get; set; }

        public IntegrityStatus Content { get; set; }

        public IntegrityStatus Name { get; set; }

        public IntegrityStatus ReplyTo { get; set; }

        public IntegrityStatus FromTo { get; set; }

        public IntegrityStatus Maillist { get; set; }

        public IntegrityStatus Segments { get; set; }

        public IntegrityStatus SpamRate { get; set; }
    }

    public class IntegrityStatus
    {
        public long Status { get; set; }
        public string Error { get; set; }
    }

    public class Maillists
    {
        public List<MailListModel> Rows { get; set; }

        public long Count { get; set; }

        public long TotalCount { get; set; }
    }

    public class Schedule
    {
        public string ScheduleSendDate { get; set; }

        public string ScheduleSendDateFormated { get; set; }

        public List<object> ScheduleErrorStatus { get; set; }

        public string ScheduleType { get; set; }
    }

    public class Statistics
    {
        public long DuplicatedRecipients { get; set; }

        public long FailedRecipients { get; set; }

        public long InvalidRecipients { get; set; }

        public string SendFinishDateTime { get; set; }

        public long SendFinishTimestamp { get; set; }

        public string SendStartDateTime { get; set; }

        public long SendStartTimestamp { get; set; }

        public long SentRecipients { get; set; }

        public long TotalRecipients { get; set; }
    }

    public class SocialShare
    {
        public Uri Facebook { get; set; }

        public Uri Twitter { get; set; }

        public Uri Linkedin { get; set; }

        public Uri GooglePlus { get; set; }

        public Uri Web { get; set; }
    }
}

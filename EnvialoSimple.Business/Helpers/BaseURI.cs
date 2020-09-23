using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EnvialoSimple.Business.Helpers
{
    public class BaseURI
    {
        private readonly string _APIURI;
        private readonly string _APIKEY;
        private readonly string _fromEmail;
        private readonly string _replyTo;
        private readonly string _subject;

        public BaseURI(IConfiguration configuration)
        {
            _APIKEY = configuration["APIKey"];
            _APIURI = configuration["API_URI"];
            _fromEmail = configuration["FromEmailDefault"];
            _replyTo = configuration["ReplyToDefault"];
            _subject = configuration["SubjectDefault"];
        }

        public string GetURI()
        {
            return _APIURI;
        }

        public string GetAPIKEY()
        {
            return "APIKey=" + _APIKEY;
        }

        public string GetFormat()
        {
            return "format=json";
        }

        public string GetFromEmailDefault()
        {
            return _fromEmail;
        }

        public string GetReplyToDefault()
        {
            return _replyTo;
        }

        public string GetSubjectDefault()
        {
            return _subject;
        }
    }
}

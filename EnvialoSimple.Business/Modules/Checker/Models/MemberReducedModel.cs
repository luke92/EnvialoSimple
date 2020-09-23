using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using EnvialoSimple.Business.Modules.Member.Models;
using Newtonsoft.Json.Linq;

namespace EnvialoSimple.Business.Modules.Checker.Models
{
    public class MemberReducedModel
    {
        public string MemberId { get; set; }

        public string Email { get; set; }

        public string BounceType { get; set; }

        public IDictionary<string, string> Information { get; set; }

        public string errors { get; set; }

        public MemberReducedModel()
        {
            Information = new Dictionary<string, string>();
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(MemberReducedModel))
            {
                string objEmail = ((MemberReducedModel)obj).Email;

                return string.Equals(objEmail, Email, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public MemberReducedModel(MemberModel memberModel)
        {
            Information = new Dictionary<string, string>();
            this.MemberId = memberModel.MemberId;
            this.Email = memberModel.Email;
            this.BounceType = GetBounceType(memberModel.BounceType);

            try
            {
                foreach (var customFieldJToken in memberModel.CustomFields.Item)
                {
                    try
                    {
                        var customField = customFieldJToken.First.ToObject<Member.Models.CustomField>();
                        var campo = customField.Title;
                        var valor = customField.Values.Option[0].Value;
                        this.Information.Add(campo, valor);
                    }
                    catch (Exception e)
                    {
                        errors += e.Message + ". ";
                    }
                }
            }
            catch (Exception e)
            {
                errors += e.Message + ". ";
            }
        }

        private string GetBounceType(string bounceType)
        {
            if (bounceType == "1") return "HARD";
            if (bounceType == "2") return "SOFT";
            return "NONE";
        }
    }

}

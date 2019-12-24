using System;
using System.Collections.Generic;
using System.Text;
using .EnvialoSimple.Business.Modules.MailList.Models;
using Newtonsoft.Json;

namespace .EnvialoSimple.Business.Modules.Member.Models
{
    public class MemberModel
    {
        public string MemberId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string BounceType { get; set; }

        public CustomFields CustomFields { get; set; }
        public string CustomFieldValue1 { get; set; }
        public string CustomFieldValue2 { get; set; }

        public MemberModel()
        {
            CustomFields = new CustomFields();
        }
    }

    public class CustomFields
    {
        public CustomFieldsItem Item { get; set; }
    }

    public class CustomFieldsItem
    {
        public CustomField CustomField1 { get; set; }

        public CustomField CustomField2 { get; set; }

        public CustomField CustomField3 { get; set; }
    }

    public class CustomField
    {
        public string CustomFieldId { get; set; }

        public string RelAdministratorId { get; set; }

        public string FieldType { get; set; }

        public string Title { get; set; }

        public string Validation { get; set; }

        public string ValidationCustomRegExp { get; set; }

        public long IsMultipleSelect { get; set; }

        public Values Values { get; set; }
    }

    public class Values
    {
        public List<ValueOption> Option { get; set; }
    }

    public class ValueOption
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public long Selected { get; set; }
    }

    public class MailLists
    {
        public ICollection<MailListItemElement> Item { get; set; }

        public long ActiveCount { get; set; }
    }

    public class MailListItemElement
    {
        public string MailListId { get; set; }

        public string SubscriptionStatus { get; set; }

        public string MailListName { get; set; }
    }
}

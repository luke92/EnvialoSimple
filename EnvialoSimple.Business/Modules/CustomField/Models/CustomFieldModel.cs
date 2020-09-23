using System;
using System.Collections.Generic;
using System.Text;

namespace EnvialoSimple.Business.Modules.CustomField.Models
{
    public class CustomFieldModel
    {
        public string CustomFieldId { get; set; }

        public string RelAdministratorId { get; set; }

        public string FieldType { get; set; }

        public string Title { get; set; }

        public string Validation { get; set; }

        public string ValidationCustomRegExp { get; set; }

        public int IsMultipleSelect { get; set; }

        public Values Values { get; set; }
    }

    public class Values
    {
        public Option[] Option { get; set; }
    }

    public class Option
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int Selected { get; set; }
    }
}

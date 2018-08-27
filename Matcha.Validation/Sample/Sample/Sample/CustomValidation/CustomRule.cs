using System;
using System.Collections.Generic;
using System.Text;
using Matcha.Validation;

namespace Sample.CustomValidation
{
    public class CustomRule : ValidationRule<string>
    {
        public CustomRule(string propertyName) : base(propertyName)
        {
            ValidationMessage = "please type 'love'";
        }

        protected override bool CheckValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return value.Contains("love");
        }
    }
}

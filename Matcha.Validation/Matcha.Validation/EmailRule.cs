using System;
using System.Text.RegularExpressions;

namespace Matcha.Validation
{
    public class EmailRule : ValidationRule<string>
    {
        public EmailRule(string propertyName) : base(propertyName)
        {
            ValidationMessage = "Invalid Email";
        }

        protected override bool CheckValue(string value)
        {
            if (value == null) return IsValid = false;

            var str = value as string;
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(str ?? throw new InvalidOperationException());

            return IsValid = match.Success;
        }
    }
}
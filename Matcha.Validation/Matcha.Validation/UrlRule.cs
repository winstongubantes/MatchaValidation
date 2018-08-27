using System.Text.RegularExpressions;

namespace Matcha.Validation
{
    public class UrlRule : ValidationRule<string>
    {
        public UrlRule(string propertyName) : base(propertyName)
        {
            ValidationMessage = "Invalid Url";
        }

        protected override bool CheckValue(string value)
        {
            var regex = new Regex(
                @"((^(http[s]?:\/\/)?([w]{3}[.])?(([a-z0-9\.]+)+(com|php))(((\/[a-z0-9]+)*(\/[a-z0-9]+\/?))*([a-z0-9]+[.](html|php|gif|png|jpg))?)$)|((^([.]\/)?((([a-z0-9]+)\/?)+|(([a-z0-9]+)\/)+([a-z0-9]+[.](html|php|gif|png|jpg))))$))");
            var match = regex.Match(value);

            return IsValid = match.Success;
        }
    }
}
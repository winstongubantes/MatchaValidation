namespace Matcha.Validation
{
    public class MatchRule : ValidationRule<string>
    {
        private string _matchedValue;

        public MatchRule(string propertyName) : base(propertyName)
        {
            ValidationMessage = $"{propertyName} does not match!";
        }

        public string MatchedValue
        {
            get => _matchedValue;
            set => SetProperty(ref _matchedValue, value);
        }

        protected override bool CheckValue(string value)
        {
            if (value == null) return IsValid = false;
            return IsValid = MatchedValue == value;
        }
    }
}
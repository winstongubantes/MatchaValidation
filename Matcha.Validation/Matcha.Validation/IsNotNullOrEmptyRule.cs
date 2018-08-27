namespace Matcha.Validation
{
    public class IsNotNullOrEmptyRule : ValidationRule<string>
    {
        public IsNotNullOrEmptyRule(string propertyName) : base(propertyName)
        {
            ValidationMessage = $"{propertyName} should not be empty!";
        }

        protected override bool CheckValue(string value)
        {
            return IsValid = !string.IsNullOrWhiteSpace(value);
        }
    }
}
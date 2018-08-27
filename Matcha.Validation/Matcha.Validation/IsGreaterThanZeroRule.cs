namespace Matcha.Validation
{
    public class IsGreaterThanZeroRule : ValidationRule<double>
    {
        public IsGreaterThanZeroRule(string propertyName) : base(propertyName)
        {
            ValidationMessage = "Value must be greater than zero";
        }

        protected override bool CheckValue(double value)
        {
            return value > 0;
        }
    }
}
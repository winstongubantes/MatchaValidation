namespace Matcha.Validation
{
    public abstract class ValidationRule<T> : ValidationBindableBase, IValidationRule
    {
        private bool _isValid = true;
        private string _validationMessage;

        protected ValidationRule(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string ValidationMessage
        {
            get => _validationMessage;
            set => SetProperty(ref _validationMessage, value);
        }

        public string PropertyName { get; }

        public  bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public bool Check(object value)
        {
            return IsValid = CheckValue((T) value);
        }

        protected abstract bool CheckValue(T value);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Matcha.Validation
{
    public class ValidationService<T> : ValidationBindableBase where T : INotifyPropertyChanged
    {
        private readonly T _bindable;
        private readonly IList<IValidationRule> _validationRules;
        private bool _hasChanges;
        private bool _isValidateOnInit;
        private bool _isValid;

        public ValidationService(T bindable, bool isValidateOnInit = false)
        {
            _bindable = bindable;
            _validationRules = new List<IValidationRule>();
            _isValidateOnInit = isValidateOnInit;

            SetSubscriptionOnObservables();
            SetInitialValue();
        }

        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public bool HasChanges
        {
            get => _hasChanges;
            set => SetProperty(ref _hasChanges, value);
        }

        private void SetInitialValue()
        {
            HasChanges = false;
        }

        public F Add<F>(Func<T, F> func) where F : IValidationRule
        {
            var validationRule = func(_bindable);
            _validationRules.Add(validationRule);

            if (_isValidateOnInit)
                IsValid = Validate();

            return validationRule;
        }

        private void SetSubscriptionOnObservables()
        {
            _bindable.PropertyChanged += Bindable_PropertyChanged;
        }

        public void Clear()
        {
            _bindable.PropertyChanged -= Bindable_PropertyChanged;
            _validationRules.Clear();
        }

        private void Bindable_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach (var validationRule in _validationRules)
                if (e.PropertyName == validationRule.PropertyName)
                {
                    var property = sender.GetType().GetProperty(e.PropertyName);
                    var value = property == null ? null : property.GetValue(sender, null);

                    validationRule.Check(value);
                    break;
                }

            HasChanges = true;
            IsValid = _validationRules.All(x => x.IsValid);
        }

        private bool Validate()
        {
            foreach (var validationRule in _validationRules)
            {
                var property = _bindable.GetType().GetProperty(validationRule.PropertyName);
                var value = property == null ? null : property.GetValue(_bindable, null);

                validationRule.Check(value);
            }

            return _validationRules.All(x => x.IsValid);
        }
    }

    public interface IValidationRule
    {
        string ValidationMessage { get; set; }
        string PropertyName { get; }
        bool IsValid { get; set; }
        bool Check(object value);
    }
}

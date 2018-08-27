using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Matcha.Validation;
using Xamarin.Forms;

namespace Sample.ViewModels
{
	public class PrismTabbedPageViewModel : BindableBase
	{
	    private ValidationService<PrismTabbedPageViewModel> _validationService;

        public PrismTabbedPageViewModel()
        {
            ValidationService = new ValidationService<PrismTabbedPageViewModel>(this, true);
            EntryNotEmptyRule = ValidationService.Add(e => new IsNotNullOrEmptyRule(nameof(e.UserName)));
            EntryPassRule = ValidationService.Add(e => new StrongPasswordRule(nameof(e.Password)));
        }

	    public IsNotNullOrEmptyRule EntryNotEmptyRule { get; }
	    public StrongPasswordRule EntryPassRule { get; }

        public ValidationService<PrismTabbedPageViewModel> ValidationService
	    {
	        get => _validationService;
	        set => SetProperty(ref _validationService, value);
	    }

	    private ICommand _validateBuilInCommand;
	    public ICommand ValidateBuilInCommand => _validateBuilInCommand ?? (_validateBuilInCommand = new DelegateCommand(() =>
	    {
	        if (ValidationService.IsValid)
	        {
	            Application.Current.MainPage.DisplayAlert("", "Validated", "Ok");
	        }
	    }));

        private string _userName;
        public string UserName
	    {
	        get => _userName;
            set => SetProperty(ref _userName, value);
        }

	    private string _password;
	    public string Password
        {
	        get => _password;
	        set => SetProperty(ref _password, value);
	    }
    }
}

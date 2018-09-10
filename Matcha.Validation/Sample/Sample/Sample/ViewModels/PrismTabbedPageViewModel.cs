using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Matcha.Validation;
using Sample.CustomValidation;
using Xamarin.Forms;
using Prism.Services;

namespace Sample.ViewModels
{
	public class PrismTabbedPageViewModel : BindableBase
	{
	    private ValidationService<PrismTabbedPageViewModel> _validationService;
        private readonly IPageDialogService _pageDialogService;

        public PrismTabbedPageViewModel(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            ValidationService = new ValidationService<PrismTabbedPageViewModel>(this, true);
            EntryNotEmptyRule = ValidationService.Add(e => new IsNotNullOrEmptyRule(nameof(e.UserName)));
            EntryPassRule = ValidationService.Add(e => new StrongPasswordRule(nameof(e.Password)));
            CustomRule = ValidationService.Add(e => new CustomRule(nameof(e.ForCustomValue)));
            EmailRule = ValidationService.Add(e => new EmailRule(nameof(e.Email)));
        }

	    public IsNotNullOrEmptyRule EntryNotEmptyRule { get; }
	    public StrongPasswordRule EntryPassRule { get; }
	    public CustomRule CustomRule { get; }
	    public EmailRule EmailRule { get; }

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
	            _pageDialogService.DisplayAlertAsync("", "Validated", "Ok");
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

	    private string _email;
	    public string Email
        {
	        get => _email;
	        set => SetProperty(ref _email, value);
	    }

        private string _forCustomValue;
	    public string ForCustomValue
        {
	        get => _forCustomValue;
	        set => SetProperty(ref _forCustomValue, value);
	    }
    }
}

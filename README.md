# Matcha Validation Plugin an Unobtrusive Validation for Xamarin.Forms
A plugin library for unobtrusive "Testable" validation that works well with any MVVM framework , You can create a Customized Rule which perfectly fit to your needs.
 
 ## Nuget
 [Matcha.Validation](http://www.nuget.org/packages/Matcha.Validation) [![NuGet](https://img.shields.io/nuget/v/Matcha.Validation.svg?label=NuGet)](https://www.nuget.org/packages/Matcha.Validation/)
 
 ## Preview
  ![alt text](https://github.com/winstongubantes/matcha.validation/blob/master/Images/valid.gif "Sample In Action")
  
 ### Get Started
 
 #### Using ValidationService
 
 With ValidationService you can easily validate the viewmodel that derives from any MVVM framework base viewmodel class, this makes it very powerful and testable at the same time.
 
 ```csharp
 
 public class PrismTabbedPageViewModel : BindableBase
 {
	private ValidationService<PrismTabbedPageViewModel> _validationService;

	public PrismTabbedPageViewModel()
	{
		ValidationService = new ValidationService<PrismTabbedPageViewModel>(this);
	}

	public ValidationService<PrismTabbedPageViewModel> ValidationService
	{
		get => _validationService;
		set => SetProperty(ref _validationService, value);
	}
 }
 
 ```
 
 ### ValidationRule<T>
 
 When deriving ValidationRule<T>, this will enables validation of the viewmodels property, The validation could be anything from email, null, greaterthanzero or it could be your custom validation (take EmailRule as an example).
 
 ```csharp
 
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
 
 ```
 
 ### Adding the ValidationRule<T> to ValidationService
 
 ValidationRule<T> is a bindable class this what makes the Matcha Validation so powerful because of its pure unobtrusive and it doesnt require custom control to handle it.
 
 ```csharp
 
 public PrismTabbedPageViewModel()
 {
	ValidationService = new ValidationService<PrismTabbedPageViewModel>(this);
	EntryNotEmptyRule = ValidationService.Add(e => new IsNotNullOrEmptyRule(nameof(e.UserName))); //
 }
 
 ```
 
 ### Check if all Rule is Valid
 
 ValidationRule<T> is a bindable class this what makes the Matcha Validation so powerful because of its pure unobtrusive and it doesnt require custom control to handle it.
 
 ```csharp
 
 if (ValidationService.IsValid)
 {
	//You code here when all rule's are valid
 }
 
 ```
 
 #### XAML Page
 ```xml
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage
    x:Class="Sample.Views.PrismTabbedPage"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:converter="clr-namespace:Sample.Converter;assembly=Sample"
    xmlns:prism="clr-namespace:Prism.Mvvm;assembly=Prism.Forms"
    prism:ViewModelLocator.AutowireViewModel="True">
    <TabbedPage.Resources>
        <ResourceDictionary>
            <converter:InverseBooleanConverter x:Key="BooleanConverter" />
        </ResourceDictionary>
    </TabbedPage.Resources>
    <ContentPage Title="Custom Validation">
        <StackLayout Padding="30">
            <Entry Placeholder="Username" Text="{Binding UserName}" />
            <Label
                FontSize="Micro"
                IsVisible="{Binding EntryNotEmptyRule.IsValid, Converter={StaticResource BooleanConverter}}"
                Text="{Binding EntryNotEmptyRule.ValidationMessage}"
                TextColor="Red" />
            <Button
                Command="{Binding ValidateBuilInCommand}"
                IsEnabled="{Binding ValidationService.IsValid}"
                Text="Validate" />
        </StackLayout>
    </ContentPage>
</TabbedPage>

 ```
 
 ## Platform Supported

|Platform|Version|
| ------------------- | :-----------: |
|Xamarin.iOS|iOS 7+|
|Xamarin.Android|API 15+|
|Windows 10 UWP|10+|
|Xamarin.Mac|All|
|Xamarin.tvOS|All|
|Xamarin.watchOS|All|
|.NET Standard|2.0+|
|.NET Core|2.0+|

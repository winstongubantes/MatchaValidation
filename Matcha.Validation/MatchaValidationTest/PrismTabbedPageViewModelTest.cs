using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Prism.Services;

namespace MatchaValidationTest
{
    [TestClass]
    public class PrismTabbedPageViewModelTest
    {
        private PrismTabbedPageViewModel _viewModel;
        private Mock<IPageDialogService> _pageDialogService;

        [TestInitialize]
        public void Init()
        {
            _pageDialogService = new Mock<IPageDialogService>();
            _viewModel = new PrismTabbedPageViewModel(_pageDialogService.Object);
        }

        [TestMethod]
        public void ValidateBuilInCommand_Should_Success_Validate_When_All_Input_Is_Valid()
        {
            //Arrange
            _pageDialogService.Setup(e =>
                e.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));


            //Act
            _viewModel.UserName = "test@gmail.com";
            _viewModel.Email = "test@gmail.com";
            _viewModel.Password = "test123123@";
            _viewModel.ForCustomValue = "love";
            _viewModel.ValidateBuilInCommand?.Execute(null);

            //Assert
            Assert.IsTrue(_viewModel.ValidationService.IsValid);
            Assert.IsTrue(_viewModel.EmailRule.IsValid);
            Assert.IsTrue(_viewModel.CustomRule.IsValid);
            Assert.IsTrue(_viewModel.EntryPassRule.IsValid);
            _pageDialogService.Verify(e=> e.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public void ValidateBuilInCommand_Should_Fail_Validate_When_UserName_Is_Empty()
        {
            //Arrange

            //Act
            _viewModel.Email = "test@gmail.com";
            _viewModel.Password = "test123123@";
            _viewModel.ForCustomValue = "love";
            _viewModel.ValidateBuilInCommand?.Execute(null);

            //Assert
            Assert.IsFalse(_viewModel.ValidationService.IsValid);
            Assert.IsFalse(_viewModel.EntryNotEmptyRule.IsValid);
        }

        [TestMethod]
        public void ValidateBuilInCommand_Should_Fail_Validate_When_Email_Is_Not_Valid()
        {
            //Arrange

            //Act
            _viewModel.UserName = "test@gmail.com";
            _viewModel.Email = "testcom";
            _viewModel.Password = "test123123@";
            _viewModel.ForCustomValue = "love";
            _viewModel.ValidateBuilInCommand?.Execute(null);

            //Assert
            Assert.IsFalse(_viewModel.ValidationService.IsValid);
            Assert.IsFalse(_viewModel.EmailRule.IsValid);
        }

        [TestMethod]
        public void ValidateBuilInCommand_Should_Fail_Validate_When_Custom_Value_Is_Not_Valid()
        {
            //Arrange

            //Act
            _viewModel.UserName = "test@gmail.com";
            _viewModel.Email = "test@gmail.com";
            _viewModel.Password = "test123123@";
            _viewModel.ForCustomValue = "lov";
            _viewModel.ValidateBuilInCommand?.Execute(null);

            //Assert
            Assert.IsFalse(_viewModel.ValidationService.IsValid);
            Assert.IsFalse(_viewModel.CustomRule.IsValid);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _viewModel = null;
        }
    }
}

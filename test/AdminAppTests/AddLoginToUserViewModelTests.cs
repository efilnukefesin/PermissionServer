using AdminApp.ViewModels;
using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Shared;

namespace AdminAppTests
{
    [TestClass]
    public class AddLoginToUserViewModelTests : BaseSimpleTest
    {
        #region AddLoginToUserViewModelProperties
        [TestClass]
        public class AddLoginToUserViewModelProperties : AddLoginToUserViewModelTests
        {

        }
        #endregion AddLoginToUserViewModelProperties

        #region AddLoginToUserViewModelConstruction
        [TestClass]
        public class AddLoginToUserViewModelConstruction : AddLoginToUserViewModelTests
        {
            #region Resolve
            [TestMethod]
            public void Resolve()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();

                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>(); 

                Assert.IsNotNull(addLoginToUserViewModel);
            }
            #endregion Resolve
        }
        #endregion AddLoginToUserViewModelConstruction

        #region AddLoginToUserViewModelMethods
        [TestClass]
        public class AddLoginToUserViewModelMethods : AddLoginToUserViewModelTests
        {
            #region setupAddLoginToUserViewModel
            private void setupAddLoginToUserViewModel(AddLoginToUserViewModel addLoginToUserViewModel, string SearchText)
            {
                addLoginToUserViewModel.UnknownLogins.Add(new PermissionServer.Models.UnknownLogin("123"));
                addLoginToUserViewModel.UnknownLogins.Add(new PermissionServer.Models.UnknownLogin("345"));
                addLoginToUserViewModel.Text = SearchText;
                addLoginToUserViewModel.UpdateSearchResults();
            }
            #endregion setupAddLoginToUserViewModel

            #region ChangeText
            [TestMethod]
            public void ChangeText()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "234");

                Assert.AreEqual("Add new Sub ID", addLoginToUserViewModel.ButtonText);
            }
            #endregion ChangeText

            #region FindOne
            [TestMethod]
            public void FindOne()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "23");

                Assert.AreEqual("Add Selected Sub ID", addLoginToUserViewModel.ButtonText);
            }
            #endregion FindOne
        }
        #endregion AddLoginToUserViewModelMethods
    }

}

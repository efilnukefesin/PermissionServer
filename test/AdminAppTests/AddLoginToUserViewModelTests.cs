using AdminApp.ViewModels;
using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Mvvm;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            #region setupAddLoginToUserViewModel: does basic set up tasks
            /// <summary>
            /// does basic set up tasks
            /// </summary>
            /// <param name="addLoginToUserViewModel"></param>
            /// <param name="SearchText"></param>
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

            #region FindOneButDontSelect
            [TestMethod]
            public void FindOneButDontSelect()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "23");

                Assert.AreEqual("Add new Sub ID", addLoginToUserViewModel.ButtonText);
            }
            #endregion FindOneButDontSelect

            #region SetTextBySelectingWithoutText
            [TestMethod]
            public void SetTextBySelectingWithoutText()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "");
                addLoginToUserViewModel.SelectedUnknownLogin = addLoginToUserViewModel.UnknownLogins.Where(x => x.SubjectId.Equals("123")).FirstOrDefault();

                Assert.AreEqual("Add Selected Sub ID", addLoginToUserViewModel.ButtonText);
                Assert.AreEqual("123", addLoginToUserViewModel.Text);
            }
            #endregion SetTextBySelectingWithoutText

            #region SetTextBySelectingWithText
            [TestMethod]
            public void SetTextBySelectingWithText()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "666");
                addLoginToUserViewModel.SelectedUnknownLogin = addLoginToUserViewModel.UnknownLogins.Where(x => x.SubjectId.Equals("123")).FirstOrDefault();

                Assert.AreEqual("Add Selected Sub ID", addLoginToUserViewModel.ButtonText);
                Assert.AreEqual("123", addLoginToUserViewModel.Text);
            }
            #endregion SetTextBySelectingWithText

            #region AddBySelectingWithText
            [TestMethod]
            public void AddBySelectingWithText()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "666");
                string unknownLoginId = "123";
                addLoginToUserViewModel.SelectedUnknownLogin = addLoginToUserViewModel.UnknownLogins.Where(x => x.SubjectId.Equals(unknownLoginId)).FirstOrDefault();

                bool canExecute = addLoginToUserViewModel.AddOrCreateCommand.CanExecute(null);
                if (canExecute)
                {
                    addLoginToUserViewModel.AddOrCreateCommand.Execute(null);
                }

                Assert.AreEqual(true, canExecute);
                Assert.AreEqual("Add Selected Sub ID", addLoginToUserViewModel.ButtonText);
                Assert.AreEqual("123", addLoginToUserViewModel.Text);
                Assert.AreEqual(false, addLoginToUserViewModel.UnknownLogins.Any(x => x.SubjectId.Equals(unknownLoginId)));
            }
            #endregion AddBySelectingWithText

            #region AddBySettingText
            [TestMethod]
            public void AddBySettingText()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "666");

                bool canExecute = addLoginToUserViewModel.AddOrCreateCommand.CanExecute(null);
                if (canExecute)
                {
                    addLoginToUserViewModel.AddOrCreateCommand.Execute(null);
                }

                Assert.AreEqual(true, canExecute);
                Assert.AreEqual("Add Selected Sub ID", addLoginToUserViewModel.ButtonText);
                Assert.AreEqual("", addLoginToUserViewModel.Text);
                Assert.AreEqual(true, addLoginToUserViewModel.SelectedUser.Logins.Any(x => x.SubjectId.Equals("666")));
            }
            #endregion AddBySettingText
        }
        #endregion AddLoginToUserViewModelMethods
    }

}

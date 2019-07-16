using AdminApp.ViewModels;
using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Mvvm;
using PermissionServer.Core.Factories;
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
                if (addLoginToUserViewModel.SelectedUser != null)
                {
                    addLoginToUserViewModel.SelectedUser = UserBuilder.CreateUser("Sam").AddLogin(new Login("123456789")).Build();
                }
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
                Assert.IsNull(addLoginToUserViewModel.SelectedUnknownLogin);
            }
            #endregion AddBySelectingWithText

            #region AddBySelectingWithTextCancellation
            [TestMethod]
            public void AddBySelectingWithTextCancellation()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();
                DiSetup.InitializeTests();
                DiHelper.GetService<PermissionServer.SDK.Client>().AddUnkownLoginsAsync(new List<UnknownLogin>() { new UnknownLogin("123"), new UnknownLogin("345") }).GetAwaiter().GetResult();  //just to keep it synced
                AddLoginToUserViewModel addLoginToUserViewModel = DiHelper.GetService<AddLoginToUserViewModel>();
                //this.setupAddLoginToUserViewModel(addLoginToUserViewModel, "666");
                addLoginToUserViewModel.SelectedUser = UserBuilder.CreateUser("Sam").AddLogin(new Login("123456789")).Build();
                addLoginToUserViewModel.LoadedCommand.Execute(null);
                string unknownLoginId = "123";
                addLoginToUserViewModel.SelectedUnknownLogin = addLoginToUserViewModel.UnknownLogins.Where(x => x.SubjectId.Equals(unknownLoginId)).FirstOrDefault();

                int numberOfUnkownloginsBefore = addLoginToUserViewModel.UnknownLogins.Count();

                bool canExecuteAddOrCreate = addLoginToUserViewModel.AddOrCreateCommand.CanExecute(null);
                if (canExecuteAddOrCreate)
                {
                    addLoginToUserViewModel.AddOrCreateCommand.Execute(null);
                }

                int numberOfUnkownloginsBetween = addLoginToUserViewModel.UnknownLogins.Count();

                bool canExecuteCancel = addLoginToUserViewModel.CancelCommand.CanExecute(null);
                if (canExecuteCancel)
                {
                    addLoginToUserViewModel.CancelCommand.Execute(null);
                }
                int numberOfUnkownloginsAfter = addLoginToUserViewModel.UnknownLogins.Count();

                //TODO: check server side of things, data service
                //TODO: check double creation of items

                Assert.AreEqual(true, canExecuteAddOrCreate);
                Assert.AreEqual(true, canExecuteCancel);
                Assert.AreEqual("Add Selected Sub ID", addLoginToUserViewModel.ButtonText);
                Assert.AreEqual("123", addLoginToUserViewModel.Text);
                Assert.AreEqual(numberOfUnkownloginsBefore, numberOfUnkownloginsAfter);
                Assert.AreEqual(numberOfUnkownloginsBefore, numberOfUnkownloginsBetween + 1);
                Assert.AreEqual(numberOfUnkownloginsAfter, numberOfUnkownloginsBetween + 1);
                Assert.AreEqual(true, addLoginToUserViewModel.UnknownLogins.Any(x => x.SubjectId.Equals(unknownLoginId)));
                Assert.IsNull(addLoginToUserViewModel.SelectedUnknownLogin);
            }
            #endregion AddBySelectingWithTextCancellation

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
                Assert.AreEqual("Add new Sub ID", addLoginToUserViewModel.ButtonText);
                Assert.AreEqual("", addLoginToUserViewModel.Text);
                Assert.AreEqual(true, addLoginToUserViewModel.SelectedUser.Logins.Any(x => x.SubjectId.Equals("666")));
                Assert.IsNull(addLoginToUserViewModel.SelectedUnknownLogin);
            }
            #endregion AddBySettingText
        }
        #endregion AddLoginToUserViewModelMethods
    }
}

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

        }
        #endregion AddLoginToUserViewModelMethods
    }

}

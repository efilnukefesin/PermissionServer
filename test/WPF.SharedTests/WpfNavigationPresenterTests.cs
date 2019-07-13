using BootStrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Shared;

namespace WPF.SharedTests
{
    [TestClass]
    public class WpfNavigationPresenterTests : BaseSimpleTest
    {
        #region WpfNavigationPresenterProperties
        [TestClass]
        public class WpfNavigationPresenterProperties : WpfNavigationPresenterTests
        {

        }
        #endregion WpfNavigationPresenterProperties

        #region WpfNavigationPresenterConstruction
        [TestClass]
        public class WpfNavigationPresenterConstruction : WpfNavigationPresenterTests
        {
            #region Resolve
            [TestMethod]
            public void Resolve()
            {
                DiSetup.Tests();
                DiHelper.Register<INavigationPresenter, DummyNavigationPresenter>();

                INavigationPresenter navigationPresenter = DiHelper.GetService<INavigationPresenter>();

                Assert.IsNotNull(navigationPresenter);
            }
            #endregion Resolve
        }
        #endregion WpfNavigationPresenterConstruction

        #region WpfNavigationPresenterMethods
        [TestClass]
        public class WpfNavigationPresenterMethods : WpfNavigationPresenterTests
        {

        }
        #endregion WpfNavigationPresenterMethods
    }

}

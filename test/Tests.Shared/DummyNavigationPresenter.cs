using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Shared
{
    public class DummyNavigationPresenter : BaseObject, INavigationPresenter
    {
        public bool IsPresenterRegistered { get; set; }

        #region Back
        public void Back()
        {
            
        }
        #endregion Back

        public bool Present(string ViewUri, object DataContext)
        {
            return true;
        }

        public void RegisterPresenter(object Presenter)
        {
            this.IsPresenterRegistered = true;
        }

        protected override void dispose()
        {
            
        }
    }
}

using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AdminApp.Classes
{
    //TODO: generalize
    public class WpfNavigationPresenter : BaseObject, INavigationPresenter
    {
        #region Properties

        private Frame presentationFrame;
        private object currentDataContext = null;

        private string bufferedViewUri = null;
        private object bufferedDataContext = null;

        private const string packPrefix = "pack://application:,,,/AdminApp;component/Views/";
        private const string typePrefix = "AdminApp.Views.";

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Back
        public void Back()
        {
            throw new NotImplementedException();
            //TODO: If Modal Window is shown, close; if page view is shown,  go one back, forward to Presenter Implementation.
        }
        #endregion Back

        #region Present
        public bool Present(string ViewUri, object DataContext)
        {
            bool result = false;
            try
            {
                this.currentDataContext = DataContext;
                Uri viewPackUri = new Uri(WpfNavigationPresenter.packPrefix + ViewUri);
                if (ViewUri.EndsWith("Window.xaml"))
                {
                    //show as modal window
                    string typeName = ViewUri.Replace(".xaml", "");
                    Type windowType = Type.GetType(typePrefix + typeName);
                    var window = TypeHelper.CreateInstance(windowType);
                    if (window is Window)
                    {
                        (window as Window).Owner = Application.Current.MainWindow;
                        (window as Window).DataContext = this.currentDataContext;
                        (window as Window).ShowDialog();
                    }
                }
                else
                {
                    if (this.presentationFrame != null)
                    {
                        this.presentationFrame.Navigate(viewPackUri);

                        if ((Page)this.presentationFrame.Content != null)
                        {
                            ((Page)this.presentationFrame.Content).DataContext = null;
                        }

                        this.bufferedViewUri = null;
                        this.bufferedDataContext = null;

                        result = true;
                    }
                    else
                    {
                        this.bufferedViewUri = ViewUri;
                        this.bufferedDataContext = DataContext;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion Present

        #region RegisterPresenter
        public void RegisterPresenter(object Presenter)
        {
            if (Presenter is Frame)
            {
                this.presentationFrame = (Frame)Presenter;
                this.presentationFrame.Navigated += this.presentationFrame_Navigated;
                if (this.bufferedViewUri != null && this.bufferedDataContext != null)
                {
                    this.Present(this.bufferedViewUri, this.bufferedDataContext);
                }
            }
        }
        #endregion RegisterPresenter

        #region presentationFrame_Navigated: set the datacontext after navigating as the content is now different
        /// <summary>
        /// set the datacontext after navigating as the content is now different
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void presentationFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if ((Page)e.Content != null)
            {
                ((Page)e.Content).DataContext = this.currentDataContext;
            }
        }
        #endregion presentationFrame_Navigated

        #region dispose
        protected override void dispose()
        {
            this.presentationFrame = null;
            this.currentDataContext = null;
            this.bufferedDataContext = null;
            this.bufferedViewUri = null;
        }
        #endregion dispose

        #endregion Methods   
    }
}

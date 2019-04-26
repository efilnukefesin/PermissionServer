using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SimpleMenuItem : BaseObject
    {
        #region Properties

        public string Caption { get; set; }
        public Action Action { get; set; }
        public bool IsActive { get; set; }

        #endregion Properties

        #region Construction

        public SimpleMenuItem(string Caption, Action Action, bool IsActive = true)
        {
            this.Caption = Caption;
            this.Action = Action;
            this.IsActive = IsActive;
        }

        #endregion Construction

        #region Methods

        #region Execute: invokes the action id possible
        /// <summary>
        /// invokes the action id possible
        /// </summary>
        public void Execute()
        {
            if (this.IsActive)
            {
                this.Action?.Invoke();
            }
        }
        #endregion Execute

        #region dispose
        protected override void dispose()
        {
            this.Action = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}

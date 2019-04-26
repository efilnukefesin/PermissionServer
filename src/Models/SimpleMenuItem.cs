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

        #endregion Properties

        #region Construction

        public SimpleMenuItem(string Caption, Action Action)
        {
            this.Caption = Caption;
            this.Action = Action;
        }

        #endregion Construction

        #region Methods

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

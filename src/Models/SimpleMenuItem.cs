using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SimpleMenuItem
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

        #endregion Methods

        #region Events

        #endregion Events
    }
}

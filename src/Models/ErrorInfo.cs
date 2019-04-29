using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ErrorInfo
    {
        #region Properties

        public string Text { get; set; }
        public int Number { get; set; }

        #endregion Properties

        #region Construction

        public ErrorInfo(int Number, string Text)
        {
            this.Text = Text;
            this.Number = Number;
        }

        #endregion Construction

        #region Methods

        #endregion Methods
    }
}

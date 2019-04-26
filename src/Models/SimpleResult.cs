using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    /// class representing a result value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleResult<T> : BaseObject
    {
        #region Properties

        public T Payload { get; set; }
        public bool IsError { get; set; }
        public string ErrorText { get; set; }

        #endregion Properties

        #region Construction

        public SimpleResult(T Payload)
        {
            this.IsError = false;
            this.Payload = Payload;
        }

        public SimpleResult(string ErrorText)
        {
            this.IsError = true;
            this.ErrorText = ErrorText;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Payload = default(T);
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}

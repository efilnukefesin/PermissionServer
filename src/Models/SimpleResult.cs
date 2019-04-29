using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
    [DataContract]
    /// <summary>
    /// class representing a result value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleResult<T> : BaseObject
    {
        #region Properties

        [DataMember]
        public T Payload { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string ErrorText { get; set; }

        #endregion Properties

        #region Construction

        public SimpleResult(T Payload, bool IsError = false)
        {
            this.IsError = IsError;
            this.Payload = Payload;
        }

        public SimpleResult(string ErrorText)
        {
            this.IsError = true;
            this.ErrorText = ErrorText;
        }

        public SimpleResult()
        {
            
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

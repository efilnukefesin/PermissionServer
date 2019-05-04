using Models;
using Newtonsoft.Json;
using PermissionServer.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SuperHotFeatureServer.SDK
{
    public class Client : BaseClient
    {
        #region Properties

        #endregion Properties

        #region Construction

        public Client(Uri BaseUrl, string BearerToken = null) : base(BaseUrl, BearerToken)
        {
        }

        #endregion Construction

        #region Methods

        #region GetValue
        public string GetValue()
        {
            string result = string.Empty;

            HttpResponseMessage response = this.httpClient.GetAsync("api/values").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<string> requestResult = JsonConvert.DeserializeObject<SimpleResult<string>>(json);
                if (requestResult.IsError)
                {
                    //result = requestResult.Error.Text;
                    //TODO: ErrorInfo handling
                }
                else
                {
                    result = requestResult.Payload;
                }
                //TODO: set result

            }
            else
            {
                //result = $"{(int)response.StatusCode} ({response.ReasonPhrase})";
                //result = requestResult.Error.Text;
                //TODO: ErrorInfo handling
            }
            return result;
        }
        #endregion GetValue

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}

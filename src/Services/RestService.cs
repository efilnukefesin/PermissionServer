using Interfaces;
using Models;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Services
{
    public class RestService : BaseObject, IRestService
    {
        #region Properties

        private HttpClient client = new HttpClient();

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region AddAuthenticationHeader
        public void AddAuthenticationHeader(string value, string type = "Bearer")
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }
        #endregion AddAuthenticationHeader

        #region GetUser
        public object GetUser(Uri permissionGetEndpoint)
        {
            object result = default(object);

            this.client.BaseAddress = permissionGetEndpoint;

            //Add an Accept header for JSON format
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //send the request and get the response
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<User> userResult = JsonConvert.DeserializeObject<SimpleResult<User>>(json);
                if (userResult.IsError)
                {
                    result = userResult.Error.Text;
                }
                else
                {
                    result = userResult.Payload;
                }
            }
            else
            {
                result = $"{(int)response.StatusCode} ({response.ReasonPhrase})";
            }

            return result;
        }
        #endregion GetUser

        #region dispose
        protected override void dispose()
        {
            this.client = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}

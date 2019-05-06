using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PermissionServer.Client
{
    public abstract class BaseClient : BaseObject
    {
        #region Properties

        protected HttpClient httpClient;

        #endregion Properties

        #region Construction

        public BaseClient(Uri BaseUrl, string BearerToken = null)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = BaseUrl;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (BearerToken != null)
            {
                this.AddAuthenticationHeader(BearerToken);
            }
        }

        #endregion Construction

        #region Methods

        #region AddAuthenticationHeader
        public void AddAuthenticationHeader(string value, string type = "Bearer")
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }
        #endregion AddAuthenticationHeader

        #region GetGivenPermissions
        public IEnumerable<Permission> GetGivenPermissions()
        {
            IEnumerable<Permission> result = default(IEnumerable<Permission>);

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
        #endregion GetGivenPermissions

        #region dispose
        protected override void dispose()
        {
            this.httpClient = null;
        }
        #endregion dispose

        #endregion Methods
    }
}

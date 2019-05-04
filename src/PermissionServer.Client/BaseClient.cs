using NET.efilnukefesin.Implementations.Base;
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

        #region dispose
        protected override void dispose()
        {
            this.httpClient = null;
        }
        #endregion dispose

        #endregion Methods
    }
}

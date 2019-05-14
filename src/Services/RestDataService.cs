using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RestDataService : BaseObject, IDataService
    {
        #region Properties

        protected HttpClient httpClient;

        #endregion Properties

        #region Construction

        public RestDataService(Uri BaseUri, string BearerToken = null)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = BaseUri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (BearerToken != null)
            {
                this.addAuthenticationHeader(BearerToken);
            }
        }

        #endregion Construction

        #region Methods

        #region addAuthenticationHeader
        private void addAuthenticationHeader(string value, string type = "Bearer")
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }
        #endregion addAuthenticationHeader

        #region GetAsync
        public async Task<T> GetAsync<T>()
        {
            T result = default(T);

            //TODO: implement

            return result;
        }
        #endregion GetAsync

        #region dispose
        protected override void dispose()
        {
            this.httpClient = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}

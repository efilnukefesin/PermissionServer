using Interfaces;
using Models;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
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

        public IEndpointRegister EndpointRegister { get; private set; }

        #endregion Properties

        #region Construction

        public RestDataService(Uri BaseUri, IEndpointRegister EndpointRegister, string BearerToken = null, HttpMessageHandler OverrideMessageHandler = null)
        {
            this.EndpointRegister = EndpointRegister;
            if (OverrideMessageHandler != null)
            {
                this.httpClient = new HttpClient(OverrideMessageHandler);
            }
            else
            {
                this.httpClient = new HttpClient();
            }
            this.httpClient.BaseAddress = BaseUri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (BearerToken != null)
            {
                this.addAuthenticationHeader(BearerToken);
            }
        }

        #endregion Construction

        #region Methods

        #region AddOrReplaceAuthentication
        public void AddOrReplaceAuthentication(string BearerToken)
        {
            this.addAuthenticationHeader(BearerToken);
        }
        #endregion AddOrReplaceAuthentication

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

            //TODO: build Dict: Type - Query string (+Operation? CRUD? Or per Method? Necessary?)
            //HttpResponseMessage response = await this.httpClient.GetAsync("api/permissions/givenpermissions");  //TODO: replace by config service value
            HttpResponseMessage response = await this.httpClient.GetAsync(this.EndpointRegister.GetEndpoint("GivenPermissions"));
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<T> requestResult = JsonConvert.DeserializeObject<SimpleResult<T>>(json);
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
            }
            return result;
        }
        #endregion GetAsync

        #region PostAsync
        public async Task<bool> PostAsync<T>(T Value)
        {
            bool result = false;
            throw new NotImplementedException();
            return result;
        }
        #endregion PostAsync

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

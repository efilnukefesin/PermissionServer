using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;
using Interfaces;

namespace PermissionServer.Client
{
    public abstract class BaseClient : BaseObject
    {
        #region Properties

        protected HttpClient httpClient;
        protected IDataService dataService;

        #endregion Properties

        #region Construction

        public BaseClient(IDataService DataService, Uri BaseUrl, string BearerToken = null)
        {
            this.dataService = DataService;
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

        #region GetGivenPermissionsAsync
        public async Task<IEnumerable<Permission>> GetGivenPermissionsAsync()
        {
            IEnumerable<Permission> result = default(IEnumerable<Permission>);

            HttpResponseMessage response = await this.httpClient.GetAsync("api/permissions/givenpermissions");  //TODO: replace by config service value
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<IEnumerable<Permission>> requestResult = JsonConvert.DeserializeObject<SimpleResult<IEnumerable<Permission>>>(json);
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
            }
            return result;
        }
        #endregion GetGivenPermissionsAsync

        #region dispose
        protected override void dispose()
        {
            this.httpClient = null;
        }
        #endregion dispose

        #endregion Methods
    }
}

using Interfaces;
using Models;
using Newtonsoft.Json;
using PermissionServer.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SuperHotOtherFeatureServer.SDK
{
    public class Client : BaseClient
    {
        #region Properties

        #endregion Properties

        #region Construction

        public Client(IDataService DataService, Uri BaseUrl, string BearerToken = null) : base(DataService, BaseUrl, BearerToken)
        {
        }

        #endregion Construction

        #region Methods

        #region GetValue
        public async Task<string> GetValueAsync()
        {
            string result = string.Empty;

            HttpResponseMessage response = await this.httpClient.GetAsync("api/values");  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<string> requestResult = JsonConvert.DeserializeObject<SimpleResult<string>>(json);
                if (!requestResult.IsError)
                {
                    result = requestResult.Payload;
                }
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

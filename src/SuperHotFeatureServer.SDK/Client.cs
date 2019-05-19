using Interfaces;
using Models;
using Newtonsoft.Json;
using PermissionServer.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SuperHotFeatureServer.SDK
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

        #region GetValueAsync
        public async Task<string> GetValueAsync()
        {
            string result = string.Empty;
            result = await this.dataService.GetAsync<string>("SuperHotFeatureServer.SDK.Client.GetValueAsync");
            return result;
        }
        #endregion GetValueAsync

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

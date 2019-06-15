using Interfaces;
using Models;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
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

        public Client(IDataService DataService) : base(DataService)
        {
        }

        #endregion Construction

        #region Methods

        #region GetValueAsync
        public async Task<string> GetValueAsync()
        {
            string result = string.Empty;
            var requestResult = await this.dataService.GetAsync<ValueObject<string>>("SuperHotFeatureServer.SDK.Client.GetValueAsync");
            result = requestResult.Value;
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

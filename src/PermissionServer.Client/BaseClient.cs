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

        protected IDataService dataService;

        #endregion Properties

        #region Construction

        public BaseClient(IDataService DataService)
        {
            this.dataService = DataService;
        }

        #endregion Construction

        #region Methods

        #region AddAuthenticationHeader
        public void AddAuthenticationHeader(string value, string type = "Bearer")
        {
            this.dataService.AddOrReplaceAuthentication(value);
        }
        #endregion AddAuthenticationHeader

        #region GetGivenPermissionsAsync
        public async Task<IEnumerable<Permission>> GetGivenPermissionsAsync()
        {
            IEnumerable<Permission> result = default(IEnumerable<Permission>);
            result = await this.dataService.GetAsync<IEnumerable<Permission>>("PermissionServer.Client.BaseClient.GetGivenPermissionsAsync");
            return result;
        }
        #endregion GetGivenPermissionsAsync

        #region dispose
        protected override void dispose()
        {
            this.dataService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}

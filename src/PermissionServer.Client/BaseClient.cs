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
using NET.efilnukefesin.Contracts.Services.DataService;
using System.Linq;

namespace PermissionServer.Client
{
    public abstract class BaseClient : BaseObject
    {
        #region Properties

        protected IDataService dataService;
        protected IEnumerable<Permission> currentPermissions;

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

        #region FetchPermissions
        public async Task<bool> FetchPermissions()
        {
            return await this.fetchPermissions();
        }
        #endregion FetchPermissions

        #region fetchPermissions
        private async Task<bool> fetchPermissions()
        {
            this.currentPermissions = await this.dataService.GetAsync<IEnumerable<Permission>>("PermissionServer.Client.BaseClient.fetchPermissions");
            return this.currentPermissions.Count() > 0;
        }
        #endregion fetchPermissions

        #region GetGivenPermissionsAsync
        public async Task<IEnumerable<Permission>> GetGivenPermissionsAsync()
        {
            if (this.currentPermissions.Count() == 0)
            {
                await this.fetchPermissions();
            }
            return this.currentPermissions;
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

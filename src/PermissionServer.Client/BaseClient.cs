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
    public abstract class BasePermissionClient : BaseObject
    {
        #region Properties

        protected IDataService dataService;
        protected IEnumerable<Permission> currentPermissions;
        protected IEnumerable<UserValue> currentUserValues;

        #endregion Properties

        #region Construction

        public BasePermissionClient(IDataService DataService)
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
        //TODO: move to specific client?
        protected async Task<bool> fetchPermissions()
        {
            bool result = false;

            this.currentPermissions = await this.dataService.GetAllAsync<Permission>("PermissionServer.Client.BaseClient.fetchPermissions");
            this.OnPermissionsUpdated(new EventArgs());

            if (this.currentPermissions != null && this.currentPermissions.Count() > 0)
            {
                result = true;
            }

            return result;
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
            this.currentPermissions = null;
            this.currentUserValues = null;
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #region OnPermissionsUpdated
        protected virtual void OnPermissionsUpdated(EventArgs e)
        {
            this.PermissionsUpdated?.Invoke(this, e);
        }
        #endregion OnPermissionsUpdated

        public event EventHandler PermissionsUpdated;

        #endregion Events
    }
}

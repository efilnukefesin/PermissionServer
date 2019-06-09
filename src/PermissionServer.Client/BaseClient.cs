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
        protected IEnumerable<UserValue> currentUserValues;

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

        #region FetchUserValues
        public async Task<bool> FetchUserValues()
        {
            return await this.fetchUserValues();
        }
        #endregion FetchUserValues

        #region fetchPermissions
        //TODO: move to specific client?
        protected async Task<bool> fetchPermissions()
        {
            bool result = false;

            this.currentPermissions = await this.dataService.GetAsync<IEnumerable<Permission>>("PermissionServer.Client.BaseClient.fetchPermissions");
            this.OnPermissionsUpdated(new EventArgs());

            if (this.currentPermissions != null && this.currentPermissions.Count() > 0)
            {
                result = true;
            }

            return result;
        }
        #endregion fetchPermissions

        #region fetchUserValues
        protected async Task<bool> fetchUserValues()
        {
            bool result = false;

            this.currentUserValues = await this.dataService.GetAsync<IEnumerable<UserValue>>("PermissionServer.Client.BaseClient.fetchUserValues");
            this.OnUserValuesUpdated(new EventArgs());

            if (this.currentUserValues != null && this.currentUserValues.Count() > 0)
            {
                result = true;
            }

            return result;
        }
        #endregion fetchUserValues

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

        #region GetUserValuesAsync
        public async Task<IEnumerable<UserValue>> GetUserValuesAsync()
        {
            if (this.currentUserValues.Count() == 0)
            {
                await this.fetchUserValues();
            }
            return this.currentUserValues;
        }
        #endregion GetUserValuesAsync

        #region dispose
        protected override void dispose()
        {
            this.dataService = null;
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

        #region OnUserValuesUpdated
        protected virtual void OnUserValuesUpdated(EventArgs e)
        {
            this.UserValuesUpdated?.Invoke(this, e);
        }
        #endregion OnUserValuesUpdated

        public event EventHandler PermissionsUpdated;
        public event EventHandler UserValuesUpdated;

        #endregion Events
    }
}

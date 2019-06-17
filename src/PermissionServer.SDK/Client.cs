using Interfaces;
using Models;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using PermissionServer.Client;
using PermissionServer.Core.Helpers;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionServer.SDK
{
    public class Client : BaseClient
    {
        #region Properties

        private Timer fetchPermissionsTimer;
        private Timer fetchUserValuesTimer;

        private IConfigurationService configurationService;
        private object lockSync = new object();

        #endregion Properties

        #region Construction

        public Client(IDataService DataService, IConfigurationService ConfigurationService) : base(DataService)
        {
            this.configurationService = ConfigurationService;
            this.fetchPermissionsTimer = new Timer(this.fetchPermissionsTimerCallback, null, TimeSpan.FromMilliseconds(0), this.configurationService.PermissionBufferTime);
            this.fetchUserValuesTimer = new Timer(this.fetchUserValuesTimerCallback, null, TimeSpan.FromMilliseconds(0), this.configurationService.UserValueBufferTime);
        }

        #endregion Construction

        #region Methods

        #region fetchPermissionsTimerCallback
        private async void fetchPermissionsTimerCallback(object state)
        {
            try
            {
                bool hasFetchedSuccessfully = await this.fetchPermissions();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion fetchPermissionsTimerCallback

        #region fetchUserValuesTimerCallback
        private async void fetchUserValuesTimerCallback(object state)
        {
            try
            {
                bool hasFetchedSuccessfully = await this.fetchUserValues();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion fetchUserValuesTimerCallback

        #region GetUserAsync
        public async Task<User> GetUserAsync()
        {
            User result = default;
            result = await this.dataService.GetAsync<User>("PermissionServer.SDK.Client.GetUserAsync");
            return result;
        }
        #endregion GetUser

        #region CheckPermissionAsync
        public async Task<bool> CheckPermissionAsync(string subjectId, string permission)
        {
            bool result = false;
            var requestResult = await this.dataService.GetAsync<ValueObject<bool>>("PermissionServer.SDK.Client.CheckPermissionAsync", subjectId, permission);
            if (requestResult != null)
            {
                result = requestResult.Value;
            }
            else
            {
                result = false;
            }
            return result;
        }
        #endregion CheckPermissionAsync

        #region FetchUserValues
        public async Task<bool> FetchUserValues()
        {
            return await this.fetchUserValues();
        }
        #endregion FetchUserValues

        #region fetchUserValues
        protected async Task<bool> fetchUserValues()
        {
            bool result = false;

            this.currentUserValues = await this.dataService.GetAllAsync<UserValue>("PermissionServer.SDK.Client.fetchUserValues");
            if (this.currentUserValues != null)
            {
                this.OnUserValuesUpdated(new EventArgs());

                if (this.currentUserValues != null && this.currentUserValues.Count() > 0)
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion fetchUserValues

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

        #region extractToken
        private string extractToken(Microsoft.Extensions.Primitives.StringValues HttpAuthHeader)
        {
            return HttpAuthHeader.ToString().Replace("Bearer ", "");
        }
        #endregion extractToken

        #region May
        public bool May(string PermissionName)
        {
            bool result = false;

            if (this.currentPermissions != null)
            {
                result = this.currentPermissions.Any(x => x != null && x.Name.Equals(PermissionName));
            }

            return result;
        }
        #endregion May

        #region CheckPermissionAsync
        public async Task<bool> CheckPermissionAsync(Microsoft.Extensions.Primitives.StringValues HttpAuthHeader, ClaimsPrincipal principal, string Permission)
        {
            this.AddAuthenticationHeader(this.extractToken(HttpAuthHeader));
            return await this.CheckPermissionAsync(PrincipalHelper.ExtractSubjectId(principal), Permission);
        }
        #endregion CheckPermission

        #region AddUserAsync
        public async Task<bool> AddUserAsync(User user)
        {
            bool result = false;
            result = await this.dataService.CreateOrUpdateAsync<User>("PermissionServer.SDK.Client.AddUser", user);
            return result;
        }
        #endregion AddUserAsync

        #region AddPermissionAsync
        public async Task<bool> AddPermissionAsync(Permission newPermission)
        {
            bool result = false;
            result = await this.dataService.CreateOrUpdateAsync<Permission>("PermissionServer.SDK.Client.AddPermissionAsync", newPermission);
            return result;
        }
        #endregion AddPermissionAsync

        #region GetAllPermissionsAsync
        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            IEnumerable<Permission> result = default;
            result = await this.dataService.GetAllAsync<Permission>("PermissionServer.SDK.Client.GetAllPermissionsAsync");
            return result;
        }
        #endregion GetAllUserPermissionsAsync

        #region GetAllPermissionsAsync
        public async Task<IEnumerable<Permission>> GetAllUserPermissionsAsync()
        {
            IEnumerable<Permission> result = default;
            result = await this.dataService.GetAllAsync<Permission>("PermissionServer.SDK.Client.GetAllUserPermissionsAsync");
            return result;
        }
        #endregion GetAllUserPermissionsAsync

        #region HasUserValues: determines, if there are any user values
        /// <summary>
        /// determines, if there are any user values
        /// </summary>
        /// <returns>true, if the user object knows more than zero user values</returns>
        public bool HasUserValues()
        {
            bool result = false;
            result = this.currentUserValues.Count() > 0;
            return result;
        }
        #endregion HasUserValues

        #region HasPermissions
        /// <summary>
        /// determines, if there are any permissions
        /// </summary>
        /// <returns>true, if the user object knows more than zero permissions</returns>
        public bool HasPermissions()
        {
            bool result = false;
            result = this.currentPermissions.Count() > 0;
            return result;
        }
        #endregion HasPermissions

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #region OnUserValuesUpdated
        protected virtual void OnUserValuesUpdated(EventArgs e)
        {
            this.UserValuesUpdated?.Invoke(this, e);
        }
        #endregion OnUserValuesUpdated

        public event EventHandler UserValuesUpdated;

        #endregion Events
    }
}

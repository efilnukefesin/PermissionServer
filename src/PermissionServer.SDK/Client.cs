using Interfaces;
using Models;
using NET.efilnukefesin.Contracts.Services.DataService;
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

        private IConfigurationService configurationService;
        private object lockSync = new object();

        #endregion Properties

        #region Construction

        public Client(IDataService DataService, IConfigurationService ConfigurationService) : base(DataService)
        {
            this.configurationService = ConfigurationService;
            this.fetchPermissionsTimer = new Timer(this.fetchPermissionsTimerCallback, null, TimeSpan.FromMilliseconds(0), this.configurationService.PermissionBufferTime);
        }

        #endregion Construction

        #region Methods

        #region fetchPermissionsTimerCallback
        private async void fetchPermissionsTimerCallback(object state)
        {
            bool hasFetchedSuccessfully = await this.fetchPermissions();
        }
        #endregion fetchPermissionsTimerCallback

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
            result = await this.dataService.GetAsync<bool>("PermissionServer.SDK.Client.CheckPermissionAsync", subjectId, permission);
            return result;
        }
        #endregion CheckPermissionAsync

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

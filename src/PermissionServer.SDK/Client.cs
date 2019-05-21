using Interfaces;
using Models;
using NET.efilnukefesin.Contracts.Services.DataService;
using Newtonsoft.Json;
using PermissionServer.Client;
using PermissionServer.Core.Helpers;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PermissionServer.SDK
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
            result = await this.dataService.PostAsync<User>("PermissionServer.SDK.Client.AddUser", user);
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

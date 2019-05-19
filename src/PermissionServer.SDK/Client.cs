﻿using Interfaces;
using Models;
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

        public Client(IDataService DataService, Uri BaseUrl, string BearerToken = null) : base(DataService, BaseUrl, BearerToken)
        {
        }

        #endregion Construction

        #region Methods

        #region GetUserAsync
        public async Task<User> GetUserAsync()
        {
            User result = default(User);
            result = await this.dataService.GetAsync<User>("PermissionServer.SDK.Client.GetUserAsync");
            return result;
        }
        #endregion GetUser

        #region CheckPermissionAsync
        public async Task<bool> CheckPermissionAsync(string subjectId, string permission)
        {
            bool result = false;

            //send the request and get the response
            HttpResponseMessage response = await this.httpClient.GetAsync($"api/permissions/check/{subjectId}/{permission}");  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                SimpleResult<bool> boolResult = JsonConvert.DeserializeObject<SimpleResult<bool>>(json);
                if (!boolResult.IsError)
                {
                    result = boolResult.Payload;
                }
            }

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

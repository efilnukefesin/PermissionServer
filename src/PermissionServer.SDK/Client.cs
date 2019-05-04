using Models;
using Newtonsoft.Json;
using PermissionServer.Client;
using PermissionServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace PermissionServer.SDK
{
    public class Client : BaseClient
    {
        #region Properties

        #endregion Properties

        #region Construction

        public Client(Uri BaseUrl, string BearerToken = null) : base(BaseUrl, BearerToken)
        {
        }

        #endregion Construction

        #region Methods

        #region GetUser
        public User GetUser()
        {
            User result = default(User);

            //send the request and get the response
            HttpResponseMessage response = this.httpClient.GetAsync("api/permissions").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<User> userResult = JsonConvert.DeserializeObject<SimpleResult<User>>(json);
                if (userResult.IsError)
                {
                    result = null;
                }
                else
                {
                    result = userResult.Payload;
                }
            }
            else
            {
                result = null;
            }

            return result;
        }
        #endregion GetUser

        #region CheckPermission
        public bool CheckPermission(string subjectId, string permission)
        {
            bool result = false;

            //send the request and get the response
            HttpResponseMessage response = this.httpClient.GetAsync($"api/permissions/check/{subjectId}/{permission}").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<bool> boolResult = JsonConvert.DeserializeObject<SimpleResult<bool>>(json);
                if (!boolResult.IsError)
                {
                    result = boolResult.Payload;
                }
            }

            return result;
        }
        #endregion CheckPermission

        #region extractToken
        private string extractToken(Microsoft.Extensions.Primitives.StringValues HttpAuthHeader)
        {
            return HttpAuthHeader.ToString().Replace("Bearer ", "");
        }
        #endregion extractToken

        #region extractSubjectId
        private string extractSubjectId(ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        #endregion extractSubjectId

        #region CheckPermission
        public bool CheckPermission(Microsoft.Extensions.Primitives.StringValues HttpAuthHeader, ClaimsPrincipal principal, string Permission)
        {
            this.AddAuthenticationHeader(this.extractToken(HttpAuthHeader));
            return this.CheckPermission(this.extractSubjectId(principal), Permission);
        }
        #endregion CheckPermission

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

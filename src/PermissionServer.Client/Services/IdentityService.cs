using IdentityModel.Client;
using Interfaces;
using NET.efilnukefesin.Implementations.Base;
using PermissionServer.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class IdentityService : BaseObject, IIdentityService
    {
        #region Properties

        private IConfigurationService configurationService;
        private ISessionService sessionService;

        #endregion Properties

        #region Construction

        public IdentityService(IConfigurationService ConfigurationService, ISessionService SessionService)
        {
            this.configurationService = ConfigurationService;
            this.sessionService = SessionService;
        }

        #endregion Construction

        #region Methods

        #region FetchIdentity
        public async Task<bool> FetchIdentity(string username, string password)
        {
            return await this.fetchIdentity(username, password);
        }
        #endregion FetchIdentity

        #region FetchIdentity
        public async Task<bool> FetchIdentity(string username, SecureString securePassword)
        {
            string password = new System.Net.NetworkCredential(string.Empty, securePassword).Password;
            return await this.fetchIdentity(username, password);
        }
        #endregion FetchIdentity

        #region fetchIdentity
        private async Task<bool> fetchIdentity(string username, string password)
        {
            return await this.getIdentityWithResourceOwnerPassword(username, password);
        }
        #endregion fetchIdentity

        #region getIdentityWithResourceOwnerPassword: leverages the Resource Owner password flow to get an Identity Access Token
        /// <summary>
        /// leverages the Resource Owner password flow to get an Identity Access Token
        /// </summary>
        /// <returns></returns>
        private async Task<bool> getIdentityWithResourceOwnerPassword(string username, string password)
        {
            bool result = false;

            // discover endpoints from metadata
            var client = new HttpClient();

            DiscoveryResponse disco = await client.GetDiscoveryDocumentAsync(this.configurationService.IdentityEndpoint.ToString());
            if (disco.IsError)
            {
                result = false;
            }
            else
            {
                // request token
                TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = this.configurationService.IdentityClient,
                    ClientSecret = this.configurationService.IdentityClientSecret,

                    UserName = username,
                    Password = password,
                    Scope = this.configurationService.IdentityScope
                });

                if (tokenResponse.IsError)
                {
                    result = false;
                }
                else
                {
                    this.sessionService.SetAccessToken(tokenResponse.AccessToken);
                    result = true;
                }
            }

            return result;
        }
        #endregion getIdentityWithResourceOwnerPassword

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

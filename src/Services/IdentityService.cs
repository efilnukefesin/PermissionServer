using IdentityModel.Client;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class IdentityService : IIdentityService
    {
        #region Properties

        private IConfigurationService configurationService;

        #endregion Properties

        #region Construction

        public IdentityService(IConfigurationService ConfigurationService)
        {
            this.configurationService = ConfigurationService;
        }

        #endregion Construction

        #region Methods

        #region FetchIdentity
        public bool FetchIdentity()
        {
            return this.getIdentityWithResourceOwnerPassword().Result;
        }
        #endregion FetchIdentity

        #region getIdentityWithResourceOwnerPassword
        private async Task<bool> getIdentityWithResourceOwnerPassword()
        {
            bool result = false;

            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(this.configurationService.IdentityEndpoint.ToString());
            if (disco.IsError)
            {
                result = false;
            }
            else
            {
                // request token
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = this.configurationService.Client,
                    ClientSecret = this.configurationService.ClientSecret,

                    UserName = this.configurationService.Username,
                    Password = this.configurationService.Password,
                    Scope = this.configurationService.Scope
                });

                if (tokenResponse.IsError)
                {
                    result = false;
                }
                else
                {
                    result = true;
                    //TODO: store stuff away
                }
            }

            return result;
        }
        #endregion getIdentityWithResourceOwnerPassword

        #endregion Methods

        #region Events

        #endregion Events
    }
}

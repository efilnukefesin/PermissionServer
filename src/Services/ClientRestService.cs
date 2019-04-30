using Interfaces;
using Models;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Services
{
    public class ClientRestService : BaseObject, IClientRestService
    {
        #region Properties

        private HttpClient client = new HttpClient();

        #endregion Properties

        #region Methods

        #region AddAuthenticationHeader
        public void AddAuthenticationHeader(string value, string type = "Bearer")
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, value);
        }
        #endregion AddAuthenticationHeader

        #region Get
        public T Get<T>(Uri Endpoint)
        {
            T result = default(T);

            this.client.BaseAddress = Endpoint;

            //Add an Accept header for JSON format
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //send the request and get the response
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                SimpleResult<T> requestResult = JsonConvert.DeserializeObject<SimpleResult<T>>(json);
                if (requestResult.IsError)
                {
                    //result = requestResult.Error.Text;
                    //TODO: ErrorInfo handling
                }
                else
                {
                    result = requestResult.Payload;
                }
                //TODO: set result

            }
            else
            {
                //result = $"{(int)response.StatusCode} ({response.ReasonPhrase})";
                //result = requestResult.Error.Text;
                //TODO: ErrorInfo handling
            }

            return result;
        }
        #endregion Get

        #region dispose
        protected override void dispose()
        {
            this.client = null;
        }
        #endregion dispose

        #endregion Methods
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Fitbit.Api.Abstractions.Endpoints;
using Fitbit.Api.Abstractions.Models;
using Newtonsoft.Json;

namespace Fitbit.Api
{
    public abstract class EndpointBase
    {
        private HttpClient HttpClient { get; }

        private IAuthentication Authentication { get; }

        public EndpointBase(IAuthentication authentication)
        {
            HttpClient = new HttpClient();
            Authentication = authentication;
        }

        protected virtual Task<T> PostAsync<T>(string requestUri, HttpContent content)
            where T : ResponseBase
        {
            return ExecuteRequestAuthenticatedAsync<T>(HttpMethod.Post, requestUri, content);
        }

        protected virtual Task<T> GetAsync<T>(string requestUri)
            where T : ResponseBase
        {
            return ExecuteRequestAuthenticatedAsync<T>(HttpMethod.Get, requestUri);
        }

        private async Task<T> ExecuteRequestAuthenticatedAsync<T>(HttpMethod method, string requestUri, HttpContent content = null, bool isRetry = false)
            where T : ResponseBase
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Authentication.AccessToken);

            var responseContent = string.Empty;
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };
            var response = await HttpClient.SendAsync(request);

            if (response == null)
            {
                //TODO throw ex
            }

            if (response.IsSuccessStatusCode)
            {
                responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseContent);
            }

            if (isRetry)
            {
                //TODO throw specific ex
                throw new Exception();
            }

            return await TryRetry<T>(request, response);
        }

        private async Task<T> TryRetry<T>(HttpRequestMessage request, HttpResponseMessage previousResponse)
            where T : ResponseBase
        {
            var previousResponseContent = await previousResponse.Content.ReadAsStringAsync();

            if (previousResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                var previousResponseModel = JsonConvert.DeserializeObject<T>(previousResponseContent);

                if (previousResponseModel.Errors.Any(e => e.ErrorType == "expired_token"))
                {
                    await Authentication.RefreshTokenAsync();
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Authentication.AccessToken);

                    return await ExecuteRequestAuthenticatedAsync<T>(request.Method, request.RequestUri.ToString(), request.Content, isRetry: true);
                }

                return previousResponseModel;
            }

            return JsonConvert.DeserializeObject<T>(previousResponseContent);
        }
    }
}

using Fitbit.Api.Abstractions.Endpoints;
using Fitbit.Api.Abstractions.Models.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.Api.Endpoints
{
    public class Authentication : IAuthentication
    {
        private ApplicationCredentials ApplicationCredentials { get; }

        private AuthenticationResponse AuthenticationResponse { get; set; }

        private HttpClient HttpClient { get; }

        private string _internalCodeVerifier = null;
        private string _internalState = null;

        private readonly string AuthorizeBaseUrl = "https://www.fitbit.com/oauth2/authorize";
        private readonly string TokenBaseUrl = "https://api.fitbit.com/oauth2/token";

        public string AccessToken => AuthenticationResponse?.AccessToken;

        internal Authentication(ApplicationCredentials applicationCredentials)
        {
            ApplicationCredentials = applicationCredentials;
            HttpClient = new HttpClient();
        }

        internal Authentication(AuthenticationResponse authenticationResponse)
        {
            AuthenticationResponse = authenticationResponse;
            HttpClient = new HttpClient();
        }

        public string GetCodeGrantFlowUrl(PermissionsRequestType[] scope, AuthenticationPromptType prompt = AuthenticationPromptType.None, string state = "")
        {
            var authorizationUrl = new StringBuilder(AuthorizeBaseUrl);

            AppendGeneralParameters(scope, prompt, state, authorizationUrl);

            authorizationUrl.Append($"&response_type=code");

            return authorizationUrl.ToString();
        }

        public async Task FinishCodeGrantFlowAsync(string code, ExpiryType expiresIn = ExpiryType.EightHours)
        {
            SetBasicAuthorizationHeader();

            var response = await HttpClient.PostAsync(TokenBaseUrl, new FormUrlEncodedContent(GetFormContent(code, expiresIn)));
            var responseContent = await response.Content.ReadAsStringAsync();
            var authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(responseContent);

            //TODO decode
        }

        public string GetCodeGrantFlowWithPkceUrl(PermissionsRequestType[] scope, AuthenticationPromptType prompt = AuthenticationPromptType.None, string state = "")
        {
            var authorizationUrl = new StringBuilder(AuthorizeBaseUrl);

            AppendGeneralParameters(scope, prompt, state, authorizationUrl);

            if (!string.IsNullOrWhiteSpace(_internalCodeVerifier))
            {
                //TODO throw ex
            }

            _internalCodeVerifier = Helpers.ToBase64UrlEncodedString(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString("X")));

            authorizationUrl.Append($"&response_type=code");
            authorizationUrl.Append($"&code_challenge_method=S256"); //TODO add note about not supporting 'plain'
            authorizationUrl.Append($"&code_challenge={Helpers.ToBase64UrlEncodedString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(_internalCodeVerifier)))}");

            return authorizationUrl.ToString();
        }

        public async Task<AuthenticationResponse> FinishCodeGrantFlowWithPkceAsync(string code, ExpiryType expiresIn = ExpiryType.EightHours)
        {
            SetBasicAuthorizationHeader();

            var response = await HttpClient.PostAsync(TokenBaseUrl, new FormUrlEncodedContent(GetFormContent(code, expiresIn)));
            var responseContent = await response.Content.ReadAsStringAsync();

            AuthenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(responseContent);

            return AuthenticationResponse;
        }

        public string GetImplicitGrantFlowUrl(PermissionsRequestType[] scope, AuthenticationPromptType prompt = AuthenticationPromptType.None, ExpiryType expiresIn = ExpiryType.OneDay, string state = "")
        {
            var authorizationUrl = new StringBuilder(AuthorizeBaseUrl);

            AppendGeneralParameters(scope, prompt, state, authorizationUrl);

            authorizationUrl.Append($"&response_type=token");
            authorizationUrl.Append($"&expires_in={expiresIn}");

            return authorizationUrl.ToString();
        }

        public void FinishImplicitGrantFlow(string urlFragment)
        {
            if (!urlFragment.StartsWith("#"))
            {
                //TODO throw ex
            }

            throw new NotImplementedException();
        }

        public async Task RefreshTokenAsync(ExpiryType expiresIn = ExpiryType.EightHours)
        {
            SetBasicAuthorizationHeader();

            var content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", AuthenticationResponse.RefreshToken),
                new KeyValuePair<string, string>("expires_in",  Convert.ToString((int)expiresIn))
            });
            var response = await HttpClient.PostAsync(TokenBaseUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            //TODO decode
        }

        public async Task RevokeAccessTokenAsync()
        {
            await RevokeToken(AuthenticationResponse.AccessToken);
        }

        public async Task RevokeRefreshTokenAsync()
        {
            await RevokeToken(AuthenticationResponse.RefreshToken);
        }

        private async Task RevokeToken(string token)
        {
            SetBasicAuthorizationHeader();

            var content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("token", token)
            });
            var response = await HttpClient.PostAsync(TokenBaseUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            //TODO decode
        }

        public Task IntrospectTokenAsync()
        {
            throw new NotImplementedException();
        }

        private void AppendGeneralParameters(PermissionsRequestType[] scope, AuthenticationPromptType prompt, string state, StringBuilder authorizationUrl)
        {
            authorizationUrl.Append($"?client_id={ApplicationCredentials.ClientId}");
            authorizationUrl.Append($"&scope={Uri.EscapeDataString(string.Join(" ", scope.Select(s => s.ToString().ToLower())))}");
            authorizationUrl.Append($"&redirect_uri={Uri.EscapeDataString(ApplicationCredentials.RedirectUri.ToString())}");
            authorizationUrl.Append($"&prompt={prompt.ToString().ToLower()}");

            if (ApplicationCredentials.UseOwnCsrfProtection)
            {
                state = Helpers.ToBase64UrlEncodedString(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString("X")));
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                _internalState = state;

                authorizationUrl.Append($"&state={state}");
            }
        }

        private void SetBasicAuthorizationHeader()
        {
            var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApplicationCredentials.ClientId}:{ApplicationCredentials.ClientSecret}"));

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
        }

        private IEnumerable<KeyValuePair<string, string>> GetFormContent(string code, ExpiryType expiresIn)
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            keyValuePairs.Add(new KeyValuePair<string, string>("code", code));
            keyValuePairs.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            keyValuePairs.Add(new KeyValuePair<string, string>("client_id", ApplicationCredentials.ClientId));
            keyValuePairs.Add(new KeyValuePair<string, string>("redirect_uri", ApplicationCredentials.RedirectUri.ToString()));
            keyValuePairs.Add(new KeyValuePair<string, string>("expires_in", Convert.ToString((int)expiresIn)));

            if (!string.IsNullOrWhiteSpace(_internalState))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>("state", _internalState));
            }

            if (!string.IsNullOrWhiteSpace(_internalCodeVerifier))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>("code_verifier", _internalCodeVerifier));
            }

            return keyValuePairs;
        }
    }
}

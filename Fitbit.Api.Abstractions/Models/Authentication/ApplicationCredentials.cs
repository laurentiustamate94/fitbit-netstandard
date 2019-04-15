using System;
using System.Collections.Generic;
using System.Text;

namespace Fitbit.Api.Abstractions.Models.Authentication
{
    public class ApplicationCredentials
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Uri RedirectUri { get; set; }

        public bool UseOwnCsrfProtection { get; set; }

        public ApplicationCredentials()
        {
        }

        public ApplicationCredentials(string clientId, string clientSecret, string redirectUri, bool useOwnCsrfProtection)
            : this(clientId, clientSecret, new Uri(redirectUri), useOwnCsrfProtection)
        {
        }

        public ApplicationCredentials(string clientId, string clientSecret, Uri redirectUri, bool useOwnCsrfProtection)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.RedirectUri = redirectUri;
            this.UseOwnCsrfProtection = useOwnCsrfProtection;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(ClientId))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                return false;
            }

            //TODO check for RedirectUri

            return true;
        }
    }
}

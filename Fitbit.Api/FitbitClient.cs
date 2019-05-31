using Fitbit.Api.Abstractions;
using Fitbit.Api.Abstractions.Endpoints;
using Fitbit.Api.Abstractions.Models.Authentication;
using Fitbit.Api.Endpoints;
using System;

namespace Fitbit.Api
{
    public class FitbitClient : IFitbitClient
    {
        public IAuthentication Authentication { get; }

        public IHeartRate HeartRate { get; }

        public FitbitClient(string clientId, string clientSecret, string redirectUri, bool useOwnCsrfProtection = true)
            : this(new ApplicationCredentials(clientId, clientSecret, redirectUri, useOwnCsrfProtection))
        {
        }

        public FitbitClient(ApplicationCredentials applicationCredentials)
        {
            if (!applicationCredentials.IsValid())
            {
                //TODO throw Exception
            }

            this.Authentication = new Authentication(applicationCredentials);

            this.HeartRate = new HeartRate(Authentication);
        }

        public FitbitClient(AuthenticationResponse authenticationResponse)
        {
            this.Authentication = new Authentication(authenticationResponse);

            this.HeartRate = new HeartRate(Authentication);
        }
    }
}

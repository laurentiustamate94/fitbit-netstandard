using Fitbit.Api.Abstractions.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.Api.Abstractions.Endpoints
{
    public interface IAuthentication
    {
        string AccessToken { get; }

        string GetCodeGrantFlowUrl(PermissionsRequestType[] scope, AuthenticationPromptType prompt = AuthenticationPromptType.None, string state = "");

        Task FinishCodeGrantFlowAsync(string code, ExpiryType expiresIn = ExpiryType.EightHours);

        string GetCodeGrantFlowWithPkceUrl(PermissionsRequestType[] scope, AuthenticationPromptType prompt = AuthenticationPromptType.None, string state = "");

        Task<AuthenticationResponse> FinishCodeGrantFlowWithPkceAsync(string code, ExpiryType expiresIn = ExpiryType.EightHours);

        string GetImplicitGrantFlowUrl(PermissionsRequestType[] scope, AuthenticationPromptType prompt = AuthenticationPromptType.None, ExpiryType expiresIn = ExpiryType.OneDay, string state = "");

        void FinishImplicitGrantFlow(string urlFragment);

        Task RefreshTokenAsync(ExpiryType expiresIn = ExpiryType.EightHours);

        Task RevokeAccessTokenAsync();

        Task RevokeRefreshTokenAsync();

        Task IntrospectTokenAsync();
    }
}

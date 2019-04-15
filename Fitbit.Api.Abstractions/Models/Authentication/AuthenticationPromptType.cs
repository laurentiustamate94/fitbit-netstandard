using System;
using System.Collections.Generic;
using System.Text;

namespace Fitbit.Api.Abstractions.Models.Authentication
{
    public enum AuthenticationPromptType
    {
        None,
        Consent,
        Login,
        LoginConsent
    }
}

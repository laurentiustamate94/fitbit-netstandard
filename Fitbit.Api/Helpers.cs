using System;
using System.Collections.Generic;
using System.Text;

namespace Fitbit.Api
{
    internal class Helpers
    {
        private Helpers()
        {

        }

        public static string ToBase64UrlEncodedString(byte[] data)
        {
            return Convert.ToBase64String(data)
                .Split('=')[0] // Remove any trailing '=' signs
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}

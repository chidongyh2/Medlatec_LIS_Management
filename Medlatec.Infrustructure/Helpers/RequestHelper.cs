using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace Medlatec.Infrastructure.Helpers
{
    public static class RequestHelper
    {
        public static string GetClientId(HttpContext context)
        {
            var payloadObject = ParseAccessToken(context);
            return payloadObject == null ? string.Empty : (string)payloadObject.client_id;
        }

        public static string GetSub(HttpContext context)
        {
            var payloadObject = ParseAccessToken(context);
            return payloadObject == null ? string.Empty : (string)payloadObject.sub;
        }

        public static dynamic ParseAccessToken(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out var authorization);
            if (!authorization.Any())
                return string.Empty;

            var accessToken = authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken))
                return string.Empty;

            var token = accessToken.Split(" ").LastOrDefault();
            if (string.IsNullOrEmpty(token))
                return string.Empty;

            var tokenArray = token.Split('.');
            if (tokenArray == null || !tokenArray.Any())
                return string.Empty;

            var tokenBody = tokenArray[1];
            if (string.IsNullOrEmpty(tokenBody))
                return string.Empty;

            int mod4 = tokenBody.Length % 4;
            if (mod4 > 0)
            {
                tokenBody += new string('=', 4 - mod4);
            }
            var payloadData = Convert.FromBase64String(tokenBody);
            var payloadDecode = Encoding.UTF8.GetString(payloadData);
            return JsonConvert.DeserializeObject<dynamic>(payloadDecode);
        }

    }
}

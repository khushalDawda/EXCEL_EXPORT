using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Helpers
{
    public static class Helpers
    {
        public static KeyValuePair<string, bool> TokenValidation(string token)
        {
            string stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            String Role = null;
            string UserName = null;

            if (tokenS != null)
            {
                var tokensDecrypt = tokenS.Claims.ToList();
                UserName = tokensDecrypt[0].Value;
                Role = tokensDecrypt[1].Value;
                var expiration = tokensDecrypt[3].Value;
                var DateOfCreated = new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(tokensDecrypt[4].Value));
                var dateexpirtaion = new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(expiration));

                //if (//datetime of excpire - date of created > 1)
                //{
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages
                //         = new List<string>() { "Session Expire" };

                //}
            }
            return new KeyValuePair<string, bool>(Role + "|" + UserName, true);
        }
    }
}

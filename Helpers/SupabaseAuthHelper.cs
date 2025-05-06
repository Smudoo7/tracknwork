using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace TrackNWork.Helpers
{
    public class SupabaseAuthHelper
    {
        private static readonly string SupabaseIssuer = "https://hhnzduydbzxuyusuujfc.supabase.co/auth/v1";
        private static readonly string JwksUrl = $"{SupabaseIssuer}/.well-known/jwks.json";

        public static ClaimsPrincipal ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                JwksUrl,
                new OpenIdConnectConfigurationRetriever()
            );

            var config = configManager.GetConfigurationAsync().GetAwaiter().GetResult();

            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = SupabaseIssuer,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = new[] { new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abrLDpZNefBD4vK/D/iS1Oqay8Qw3vUcKKmyc4b21FEvt44oTMX3qbnBgLs5T4glFuLdmm3JEN+Ywe9iu2Azhg==")) },
            };

            SecurityToken validatedToken;
            var principal = handler.ValidateToken(token, parameters, out validatedToken);

            return principal;
        }
    }
}
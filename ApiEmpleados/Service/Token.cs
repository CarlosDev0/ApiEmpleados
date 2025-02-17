using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEmpleados.Service
{
    public class Token : IToken
    {
        private readonly IConfiguration configuration;

        public Token(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var appSettingsToken = configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null");

            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken)) // The same key as the one that generate the token
            };
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //        var jwtTokenHandler = new JwtSecurityTokenHandler();
        //        // Validation 1 - Validation JWT token format
        //        var tokenInVerification = jwtTokenHandler.ValidateToken(token, _tokenValidationParams, out var validatedToken);

        //        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        //        {
        //            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        //            if (result == false)
        //            {
        //                Error Invalid = new Error()
        //                {
        //                    Success = false,
        //                    Errors = "Token is Invalid"
        //                };

        //                context.Items["Error"] = Invalid;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error Invalid = new Error()
        //        {
        //            Success = false,
        //            Errors = "Token does not match or may expired."
        //        };
        //        context.Items["Error"] = Invalid; // userService.GetById(userId);
        //    }
        //    await _next(context);
        //}
    }


}


//JwtMiddleware
//{
//        private readonly RequestDelegate _next;
//private readonly TokenValidationParameters _tokenValidationParams;
//public JwtMiddleware(RequestDelegate next, TokenValidationParameters
//tokenValidationParams)
//{
//    _next = next;
//    _tokenValidationParams = tokenValidationParams;
//}




//    }
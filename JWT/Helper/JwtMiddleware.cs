using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JWT.Controllers;
using JWT.Model;

namespace InventorySystem.Application.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JWTSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                attachUserToContext(context, token);
            }

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);


                var jwtToken = (JwtSecurityToken)validatedToken;
                //UserConfiguration config = new UserConfiguration();
                UserRequest config = new UserRequest();
                config.roleId = jwtToken.Claims.First(x => x.Type == "RoleId").Value != null ? Convert.ToString(jwtToken.Claims.First(x => x.Type == "RoleId").Value) : "";
                config.officeCode = jwtToken.Claims.First(x => x.Type == "OfficeCode").Value != null ? Convert.ToString(jwtToken.Claims.First(x => x.Type == "OfficeCode").Value) : "";

                if (config.roleId != "" || config.officeCode != "")
                {
                    context.Items["UserConfig"] = config;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

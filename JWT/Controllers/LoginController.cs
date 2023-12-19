using Application.Helpers;
using JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly IOptions<JWTSettings> _jwtSettings;
        public LoginController(IOptions<JWTSettings> jwtSettings)
        {
            this._jwtSettings = jwtSettings;
        }

        [HttpGet]
        public string Login()
        {
            LoginResponse response = new LoginResponse();
            
            TokenUtil tokenUtil = new TokenUtil(_jwtSettings);
            JwtTokenResponse jwtToken = new JwtTokenResponse();
            jwtToken.Token = tokenUtil.GenerateJwtToken(response);

            return jwtToken.Token;
        }

        [HttpGet("list")]
        [Authorize]
        public string Get()
        {
            return "";
        }

        
    }
}

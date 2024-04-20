using inventory.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult Login([FromBody] LoginDto user)
        {
            try
            {

                if (user is null)
                {
                    return NotFound();
                }
                if (user.UserName == "amir" && user.Password == "@amir1234")
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyForAuthenticationOfApplication"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "youtCompanyIssuer.com",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(6),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new JWTTokenResponse { Token = tokenString });
                }
                return Unauthorized();
            }
            catch (Exception)
            {

                return BadRequest("خطایی رخ داده است");
            }
        }
    }
}

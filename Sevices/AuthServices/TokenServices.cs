using BackEnd_KanBan.Models.UserModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd_KanBan.Api.Sevices.AuthServices;

public class TokenServices : ITokenServices {

    private readonly IConfiguration _configuration;
    public TokenServices(IConfiguration configuration) {
        _configuration = configuration;
    }
    public async Task<string> GetTokenAsync(User user) {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credencials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);
        var claims = new[] {
            new Claim("userid", user.Id.ToString())
        };

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credencials,
            expires: expiration
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

using Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Presentation.Configurations.JwtServices;

public class IwtService : IJwtService
{

    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _tokenExpirationInMinutes;

    public IwtService(IConfiguration configuration)
    {
        var jwtSetting = configuration.GetSection("jwt");
        _secretKey = jwtSetting["Key"]!;
        _issuer = jwtSetting["Issuer"]!;
        _audience = jwtSetting["Audience"]!;
        _tokenExpirationInMinutes = int.Parse(jwtSetting["ExpireMinutes"] ?? "30");
    }
    public string GenerateToken(string userName, string role)
    {
        //try
        //{

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.Name, userName),
        //    new Claim(ClaimTypes.Role,role),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //};

        //    var token = new JwtSecurityToken(
        //        issuer: _issuer,
        //        audience: _audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_tokenExpirationInMinutes),
        //        signingCredentials: credentials
        //        );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception(ex.Message);
        //}

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var rsaSecurityKey = new RsaSecurityKey(RSA.Create());
            var credentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenExpirationInMinutes),
                SigningCredentials = credentials
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
        catch (CryptographicException ex)
        {
            throw new Exception($"مشکل در امضای دیجیتال توکن! {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"خطای عمومی: {ex.Message}");
        }
    }
}

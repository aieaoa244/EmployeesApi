using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeesAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace EmployeesAPI.Utils;

public static class JwtUtils
{
    public static string GetToken(User user, string jwtkey, string issuer, string audience)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("UserName", user.Name)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtkey));
        var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: signature
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 4);
    }

    public static bool CheckHashed(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
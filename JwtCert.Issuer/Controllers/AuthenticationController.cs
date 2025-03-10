using JwtCert.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace JwtCert.Issuer.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(IOptions<JwtConfiguration> jwtConfiguration) : ControllerBase
{
    private readonly JwtConfiguration _jwtConfiguration = jwtConfiguration.Value;

    [HttpPost]
    public IActionResult GenerateToken()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "user.Name"),
            new(JwtRegisteredClaimNames.Sub, "user.Id"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var certificate = new X509Certificate2(_jwtConfiguration.CertificatePath, _jwtConfiguration.CertificatePassword);
        
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtConfiguration.Issuer,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.MinutesExpiration),
            SigningCredentials = new X509SigningCredentials(certificate),
            Audience = _jwtConfiguration.Audience
        });

        return Ok(tokenHandler.WriteToken(token));
    }
}

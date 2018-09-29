using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApaApi.configurations;
using ApaApi.models;
using Microsoft.IdentityModel.Tokens;

namespace ApaApi.helpers
{
    /// <summary>
    /// Classe utilitária para JWT
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// Gera um novo JWT
        /// </summary>
        /// <param name="id">O identificador do usuário</param>
        /// <param name="email">O e-mail do usuário</param>
        /// <param name="signingConfiguration">As configurações de assinatura</param>
        /// <param name="tokenConfiguration">As configurações de token</param>
        /// <returns></returns>
        public static Jwt GenerateToken(string id, string email, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Email, email)
                });

            var creationDate = DateTime.Now;
            var expirationDate = creationDate.AddDays(tokenConfiguration.Days);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfiguration.Issuer,
                Audience = tokenConfiguration.Audience,
                SigningCredentials = signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });
            var token = handler.WriteToken(securityToken);

            return new Jwt
            {
                CreationDate = creationDate,
                ExpirationDate = expirationDate,
                AccessToken = token
            };
        }
    }
}

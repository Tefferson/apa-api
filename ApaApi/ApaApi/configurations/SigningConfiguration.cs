using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ApaApi.configurations
{
    /// <summary>
    /// Disponibiliza a configuração da assinatura
    /// </summary>
    public class SigningConfiguration
    {
        /// <summary>
        /// A chave de segurança
        /// </summary>
        public SecurityKey Key { get; }

        /// <summary>
        /// As credenciais da assinatura utilizadas apra validação da assinatura
        /// </summary>
        public SigningCredentials SigningCredentials { get; }

        /// <summary>
        /// Inicialiaza uma nova instância de SigningConfiguration
        /// </summary>
        public SigningConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
                Key = new RsaSecurityKey(provider.ExportParameters(true));

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}

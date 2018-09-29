using System;

namespace ApaApi.models
{
    /// <summary>
    /// Define um JWT
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// A data de criação
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// A data de expiração
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// O JWT de acesso
        /// </summary>
        public string AccessToken { get; set; }
    }
}

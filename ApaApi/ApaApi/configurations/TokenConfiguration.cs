namespace ApaApi.configurations
{
    /// <summary>
    /// As configurações do token
    /// </summary>
    public class TokenConfiguration
    {
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Days
        /// </summary>
        public int Days { get; set; }
    }
}

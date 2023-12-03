namespace SwitchTesterApi.Settings
{
    /// <summary>
    /// Configuration class for JWT (JSON Web Token) settings.
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        /// Gets or sets the issuer of the JWT.
        /// </summary>
        public virtual required string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience of the JWT.
        /// </summary>
        public virtual required string Audience { get; set; }

        /// <summary>
        /// Gets or sets the secret key used for signing the JWT.
        /// </summary>
        public virtual required string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the expiration period of the JWT in hours.
        /// </summary>
        public virtual double HoursLife { get; set; }
    }
}

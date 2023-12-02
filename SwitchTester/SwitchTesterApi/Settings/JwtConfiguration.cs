namespace SwitchTesterApi.Settings
{
    public class JwtConfiguration
    {
        public virtual required string Issuer { get; set; }
        public virtual required string Audience { get; set; }
        public virtual required string SecretKey { get; set; }
        public virtual double HoursLife { get; set; }
    }
}

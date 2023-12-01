namespace SwitchTesterApi.Models
{
    public class User
    {
        public required string UserName { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] Salt { get; set; }
    }
}

namespace SwitchTesterApi.DTOs
{
    public class ApplicationUserUpdateDTO
    {
        public required string UserName { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}

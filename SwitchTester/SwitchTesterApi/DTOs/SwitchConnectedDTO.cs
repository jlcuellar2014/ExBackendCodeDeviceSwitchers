﻿namespace SwitchTesterApi.DTOs
{
    public class SwitchConnectedDTO
    {
        public int SwitchId { get; set; }
        public required string HostName { get; set; }
        public int Port { get; set; }
    }
}

﻿namespace SwitchTesterApi.DTOs
{
    public class DeviceConnectedDTO
    {
        public int DeviceId { get; set; }
        public required string HostName { get; set; }
        public int Port { get; set; }
    }
}

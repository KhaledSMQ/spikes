﻿namespace AadManualAuth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public object Entitlements { get; set; }
    }
}

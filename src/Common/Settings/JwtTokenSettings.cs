﻿namespace TodoMinimalApi.Common.Settings
{
    public class JwtTokenSettings
    {

        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    };
}

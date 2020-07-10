﻿using System.Net;

namespace ASP_API_Template.Configuration
{
    public class HttpOptions
    {
        public string Address { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 10000;

        public IPAddress GetAddress()
            => IPAddress.Parse(Address);

        public static HttpOptions Default
            => new HttpOptions();

    }
}
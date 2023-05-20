﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Tools
{
    public class JwtTokenDefaults
    {
        public const string Issuer = "http://localhost";
        public const string Audience = "http://localhost";
        public const string Key = "yavuzyavuzyavuz1.";
        public const int Expire = 5;
    }
}

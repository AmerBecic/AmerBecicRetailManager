﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABDataManager.Models
{
    public class AuthenticatedUser
    {
        public string Access_Token { get; set; }
        public string UserName { get; set; }
    }
}
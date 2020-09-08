using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class AccountInfo
    {
        public string UserId { get; set; }
        public string Jwt { get; set; }
        public DateTime Expiration { get; set; }
    }
}

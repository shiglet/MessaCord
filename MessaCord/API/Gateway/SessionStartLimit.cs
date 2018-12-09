using System;
using System.Collections.Generic;
using System.Text;

namespace MessaCord.API.Gateway
{
    public class SessionStartLimit
    {
        public int Total { get; set; }
        public int Remaining { get; set; }
        public int ResetAfter { get; set; }
    }
}

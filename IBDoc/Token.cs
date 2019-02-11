using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDoc
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int epires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDoc
{
    public class Results
    {
        public int patient_id { get; set; }
        public String criteria { get; set; }
        public string result { get; set; }
        public float moderate_threshold { get; set; }
        public DateTime date { get; set; }
        public float high_threshold { get; set; }
        public string batch { get; set; }
        public float value { get; set; }
        public DateTime date_received { get; set; }
        public Boolean result_blinded { get; set; }
        public Boolean archived { get; set; }
        public ErrorCode error_code { get; set; }
        public float lower_limit { get; set; }
        public float upper_limit { get; set; }
        public string formatted_value { get; set; }
        
    }
}

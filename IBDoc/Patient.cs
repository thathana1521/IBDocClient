using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IBDoc
{
    public class Patient
    {
        public string url { get; set; } 
        public int id { get; set; } 
        public float moderate_threshold { get; set; } 
        public float high_threshold { get; set; } 
        public string api_key { get; set; } 
        public Boolean enabled { get; set; } 
        public string email { get; set; } 
        public string patient_name { get; set; } 
        public string patient_initials { get; set; } 
        public Boolean results_blinded { get; set; } 
        public string telephone { get; set; } 
        public Boolean send_password_email { get; set; }

    }
}

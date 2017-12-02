using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspberryControl
{
    public class Request
    {
        public string command { get; set; }
        public Hashtable parameters { get; set; }
    }
}

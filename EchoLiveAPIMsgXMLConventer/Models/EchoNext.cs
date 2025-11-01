using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    public class EchoNext : EchoLiveBasicAPI
    {
        public EchoNext() 
        {
            action = "echo_next";
        }
    }
}

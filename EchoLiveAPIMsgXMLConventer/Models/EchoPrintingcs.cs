using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    public class EchoPrintingcs : EchoLiveBasicAPI
    {
        public EchoPrintingcs(string username, string message) 
        {
            action = "echo_printing";
            data = new PrintingData() {
                Username = username,
                Message = message
            };
        }
    }

    public class PrintingData
    {
        public required string Username { get; set; }
        public required string Message { get; set; }
    }
}

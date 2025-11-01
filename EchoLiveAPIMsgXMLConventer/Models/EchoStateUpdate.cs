using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    public class EchoStateUpdate : EchoLiveBasicAPI
    {
        public EchoStateUpdate() 
        {
            action = "echo_state_update";
        }
    }

    public class StateUpdateData
    {
        public required string State { get; set; }
        public required int MessagesCount { get; set; }
    }
}

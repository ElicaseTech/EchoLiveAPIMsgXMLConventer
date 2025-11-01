using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    public class Close : EchoLiveBasicAPI
    {
        public Close() 
        {
            action = "close";
        }
    }
}

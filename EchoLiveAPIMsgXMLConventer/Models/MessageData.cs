using EchoLiveAPIMsgXMLConventer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    public class MessageData : EchoLiveBasicAPI
    {
        public MessageData(string userName, string XMLMsg) 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // 反序列化时忽略大小写
            };
            Dictionary<string, object>? msg = XMLMessageConvert.ConvertXMlToJsonDOM(XMLMsg);
            if (msg == null) 
            {
                return;
            }
            action = "message_data";
            data = new Message()
            {
                Username = userName,
                Messages = [msg]
            };
        }
    }

    public class Message
    {
        public required string Username { get; set; }
        public required List<Dictionary<string, object>> Messages { get; set; }
    }
}

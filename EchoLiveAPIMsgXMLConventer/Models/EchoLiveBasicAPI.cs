using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class EchoLiveBasicAPI
    {
        public string action { get; init; }
        public string target { get; set; }
        public EchoLiveFrom from { get; set; }
        public object data { get; set; }

        public class EchoLiveFrom
        {
            public string name { get; set; }
            public string uuid { get; set; }
            public string type { get; set; }
            public long timestamp { get; set; }
        }

        /// <summary>
        /// 将当前对象序列化为 JSON 字符串。
        /// </summary>
        public string GetJsonStr()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(this, options);
        }

        /// <summary>
        /// 从 JSON 字符串反序列化为 EchoLiveBasicAPI 对象。
        /// </summary>
        public static EchoLiveBasicAPI FromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // 反序列化时忽略大小写
            };
            return JsonSerializer.Deserialize<EchoLiveBasicAPI>(json, options);
        }
    }

}

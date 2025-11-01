using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EchoLiveAPIMsgXMLConventer.Tools
{
    public class XMLMessageConvert
    {
        public static string ConvertXmlToJson(string xml)
        {
            var doc = XDocument.Parse(xml);
            var root = doc.Root!;

            var message = new List<Dictionary<string, object>>();
            foreach (var node in root.Nodes())
            {
                message.AddRange(ParseNode(node, new Dictionary<string, object>(), new Dictionary<string, object>()));
            }

            var result = new Dictionary<string, object>
            {
                ["message"] = message
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(result, options);
        }

        public static Dictionary<string, object> ConvertXMlToJsonDOM(string xml)
        {
            var doc = XDocument.Parse(xml);
            var root = doc.Root!;

            var message = new List<Dictionary<string, object>>();
            foreach (var node in root.Nodes())
            {
                message.AddRange(ParseNode(node, new Dictionary<string, object>(), new Dictionary<string, object>()));
            }

            var result = new Dictionary<string, object>
            {
                ["message"] = message
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return result;
        }

        // 递归解析 XML 节点
        private static List<Dictionary<string, object>> ParseNode(XNode node, Dictionary<string, object> inheritedStyle, Dictionary<string, object> inheritedData)
        {
            var result = new List<Dictionary<string, object>>();

            if (node is XText textNode)
            {
                // 文本节点
                string text = textNode.Value;
                if (!string.IsNullOrEmpty(text))
                {
                    var entry = new Dictionary<string, object> { ["text"] = textNode.Value };
                    if (inheritedStyle.Count > 0)
                        entry["style"] = new Dictionary<string, object>(inheritedStyle);
                    if (inheritedData.Count > 0)
                        entry["data"] = new Dictionary<string, object>(inheritedData);
                    result.Add(entry);
                }
            }
            else if (node is XElement element)
            {
                var newStyle = new Dictionary<string, object>(inheritedStyle);
                var newData = new Dictionary<string, object>(inheritedData);
                ApplyStyleFromTagAndAttributes(element, newStyle, newData);

                var childNodes = element.Nodes();
                bool hasTextOrChild = false;

                foreach (var child in childNodes)
                {
                    var sub = ParseNode(child, newStyle, newData);
                    if (sub.Count > 0)
                    {
                        result.AddRange(sub);
                        hasTextOrChild = true;
                    }
                }

                // ✅ 如果没有任何子节点文本，也要生成空 text
                if (!hasTextOrChild)
                {
                    var entry = new Dictionary<string, object>
                    {
                        ["text"] = "",
                        ["style"] = new Dictionary<string, object>(newStyle),
                        ["data"] = new Dictionary<string, object>(newData)
                    };
                    result.Add(entry);
                }
            }

            return result;
        }

        // ✅ 核心函数：根据标签名和属性生成嵌套 style
        private static void ApplyStyleFromTagAndAttributes(XElement element, Dictionary<string, object> style, Dictionary<string, object> data)
        {
            string tag = element.Name.LocalName.ToLower();

            switch (tag)
            {
                // === 纯样式标签（布尔）===
                case "bold":
                case "italic":
                case "underline":
                case "strikethrough":
                    style[tag] = true;
                    break;

                // === 特殊样式标签，带参数 ===
                case "color":
                case "backgroundColor":
                case "size":
                case "weight":
                case "letterSpacing":
                case "stretch":
                case "style":
                case "class":
                    {
                        foreach (var attr in element.Attributes())
                        {
                            if (attr.Name.LocalName == "value")
                            {
                                style[tag] = ParseValue(attr.Value);
                            }
                        }
                        break;
                    }
                case "printSpeed":
                case "printEnd":
                case "emoji":
                    {
                        foreach (var attr in element.Attributes())
                        {
                            if (attr.Name.LocalName == "value")
                            {
                                data[tag] = ParseValue(attr.Value);
                            }
                        }
                        break;
                    }
                case "shadow":
                    {
                        var subStyle = new Dictionary<string, object>();
                        List<string> shadowValue = new List<string> { "x", "y", "blur", "color" };
                        foreach (var attr in element.Attributes())
                        {
                            if (shadowValue.Any(s => s == attr.Name.LocalName))
                            {
                                subStyle[attr.Name.LocalName] = ParseValue(attr.Value);
                            }
                        }
                        style[tag] = subStyle;
                        break;
                    }
                case "image":
                    {
                        var subImgData = new Dictionary<string, object>();
                        List<string> ImgValue = new List<string> { "url", "margin", "rendering", "size" };
                        foreach (var attr in element.Attributes())
                        {
                            if (ImgValue.Any(s => s == attr.Name.LocalName))
                            {
                                switch (attr.Name.LocalName)
                                {
                                    case "url":
                                        subImgData[attr.Name.LocalName] = attr.Value;
                                        break;
                                    case "margin":
                                        string[] marginData = attr.Value.Split(',');
                                        var subMarginData = new Dictionary<string, object>
                                        {
                                            ["left"] = marginData[0],
                                            ["right"] = marginData[1]
                                        };
                                        subImgData[attr.Name.LocalName] = subMarginData;
                                        break;
                                }
                            }
                        }
                        data[tag] = subImgData;
                        break;
                    }
                case "character":
                    {
                        var subCharacterData = new Dictionary<string, object>();
                        List<string> characterValue = new List<string> { "custom", "image", "effect" };
                        foreach (var attr in element.Attributes())
                        {
                            if (characterValue.Any(s => s == attr.Name.LocalName))
                            {
                                switch (attr.Name.LocalName)
                                {
                                    case "custom":
                                        subCharacterData[attr.Name.LocalName] = ParseValue(attr.Value);
                                        break;
                                    case "image":
                                        string[] imageData = attr.Value.Split(',');
                                        var subImageData = new Dictionary<string, object>
                                        {
                                            ["url"] = imageData[0],
                                            ["position"] = imageData[1],
                                            ["size"] = imageData[2],
                                            ["repeat"] = imageData[3]
                                        };
                                        subCharacterData[attr.Name.LocalName] = subImageData;
                                        break;
                                    case "effect":
                                        string[] effectData = attr.Value.Split(',');
                                        var subEffectData = new Dictionary<string, object>
                                        {
                                            ["name"] = effectData[0],
                                            ["duration"] = ParseValue(effectData[1]),
                                            ["scale"] = ParseValue(effectData[2]),
                                            ["timingFunction"] = effectData[3]
                                        };
                                        subCharacterData[attr.Name.LocalName] = subEffectData;
                                        break;
                                }
                            }
                        }
                        break;
                    }
            }
        }

        // ✅ 自动类型转换
        private static object ParseValue(string value)
        {
            if (bool.TryParse(value, out var b)) return b;
            if (int.TryParse(value, out var i)) return i;
            if (double.TryParse(value, out var d)) return d;
            return value; // 默认字符串
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoLiveAPIMsgXMLConventer.Models
{
    public class EchoLiveMessageModel
    {
        public EchoLiveMessageNode[]? Message { get; set; }
    }

    public class EchoLiveMessageNode
    {
        public required string Text { get; set; }
        public string? Event { get; set; }
        public string? Ruby { get; set; }
        public string? Typewrite { get; set; }
        public int? Pause { get; set; }
        public EchoLiveMessageStyle? Style { get; set; }
        public EchoLiveMessageData? Data { get; set; }
    }

    public class EchoLiveMessageStyle
    {
        public bool? Bold { get; set; }
        public bool? Italic { get; set; }
        public bool? Underline { get; set; }
        public bool? Strikethrough { get; set; }
        public bool? Emphasis { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Size { get; set; }
        public string? Weight { get; set; }
        public string? LetterSpacing { get; set; }
        public string? Stretch { get; set; }
        public string? Style { get; set; }
        public string? Class { get; set; }
        public StyleShadow? Shadow { get; set; }
    }

    public class StyleShadow
    {
        public string? X { get; set; }
        public string? Y { get; set; }
        public string? Blur { get; set; }
        public string? Color { get; set; }
    }

    public class EchoLiveMessageData 
    {
        public int PrintSpeed { get; set; }
        public string? PrintEnd { get; set; }
        public string? Emoji { get; set; }
        public DataImage? Image { get; set; }
        public DataCharacter? Character { get; set; }
    }

    public class DataImage
    {
        public string? Url { get; set; }
        public string? Rendering { get; set; }
        public DataImageMargin? Margin { get; set; }
        public DataImageSize? Size { get; set; }
    }

    public class DataImageMargin
    {
        public string? Left { get; set; }
        public string? Right { get; set; }
    }

    public class DataImageSize
    {
        public DataImageSizeData? Height { get; set; }
        public DataImageSizeData? Width { get; set; }
    }

    public class DataImageSizeData
    {
        public string? Max { get; set; }
        public string? Min { get; set; }
    }

    public class DataCharacter
    {
        public bool Custom { get; set; }
        public DataCharacterImage? Image { get; set; }
        public DataCharacterEffect? Effect { get; set; }
    }

    public class DataCharacterImage
    {
        public string? Url { set; get; }
        public string? Position { set; get; }
        public string? Size { set; get; }
        public string? Repeat { set; get; }
    }

    public class DataCharacterEffect
    {
        public string? Name { set; get; }
        public int Duration { set; get; }
        public int Scale { set; get; }
        public string? TimingFunction { set; get; }
    }
}
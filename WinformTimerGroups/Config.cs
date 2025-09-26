using System.Collections.Generic;
using System.Xml.Serialization;

namespace WinformTimerGroups
{
    [XmlRoot("Config")]
    public class Config
    {
        public Style Style { get; set; }

        [XmlElement("Category")]
        public List<Category> Categories { get; set; }
    }

    public class Style
    {
        public ButtonStyle CategoryButton { get; set; }
        public ButtonStyle GroupButton { get; set; }
        public ButtonStyle TimerButton { get; set; }
    }

    public class ButtonStyle
    {
        [XmlAttribute] public int Width { get; set; }
        [XmlAttribute] public int Height { get; set; }
        [XmlAttribute] public string FontName { get; set; }
        [XmlAttribute] public float FontSize { get; set; }
    }

    public class Category
    {
        [XmlAttribute] public string Name { get; set; }
        [XmlElement("Group")] public List<Group> Groups { get; set; }
    }

    public class Group
    {
        [XmlAttribute] public string Name { get; set; }
        [XmlElement("Timer")] public List<TimerInfo> Timers { get; set; }
    }

    public class TimerInfo
    {
        [XmlAttribute] public string ButtonText { get; set; }
        [XmlAttribute] public int CountdownSeconds { get; set; }
        [XmlAttribute] public string Description { get; set; }
        [XmlAttribute] public string StartWith { get; set; }
    }
}

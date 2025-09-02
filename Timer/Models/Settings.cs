using System.Collections.Generic;
using System.Xml.Serialization;

namespace TimerUtility.Models
{
    [XmlRoot("r")]
    public class Settings
    {
        [XmlElement("u")]
        public Profile Profile { get; set; }

        [XmlElement("h")]
        public List<Instance> Instances { get; set; } = new List<Instance>();
    }

    public class Profile
    {
        [XmlIgnore]
        public bool PlusFlag { get; set; }

        [XmlIgnore]
        public bool MinusFlag { get; set; }

        [XmlIgnore]
        public bool MillisecondsFlag { get; set; }

        [XmlIgnore]
        public int Offset { get; set; } = 1000;

        [XmlAttribute("p")]
        public string PlusFlagStr
        {
            get => PlusFlag ? "1" : "0";
            set => PlusFlag = value == "1";
        }

        [XmlAttribute("m")]
        public string MinusFlagStr
        {
            get => MinusFlag ? "1" : "0";
            set => MinusFlag = value == "1";
        }

        [XmlAttribute("ms")]
        public string MillisecondsFlagStr
        {
            get => MillisecondsFlag ? "1" : "0";
            set => MillisecondsFlag = value == "1";
        }

        [XmlAttribute("o")]
        public string OffsetStr
        {
            get => Offset.ToString();
            set => Offset = int.TryParse(value, out int result) ? result : 1000;
        }
    }

    public class Instance
    {
        [XmlAttribute("n")]
        public string Name { get; set; }

        [XmlElement("b")]
        public List<Boss> Bosses { get; set; } = new List<Boss>();
    }

    public class Boss
    {
        [XmlIgnore]
        public string InstanceName { get; set; }

        [XmlAttribute("n")]
        public string Name { get; set; }

        [XmlElement("s")]
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }

    public class Skill
    {
        [XmlIgnore]
        public string InstanceName { get; set; }

        [XmlIgnore]
        public string BossName { get; set; }

        [XmlAttribute("n")]
        public string Name { get; set; } = "计时";

        [XmlIgnore]
        public string Reset { get; set; } = "重置";

        [XmlIgnore]
        public int Interval { get; set; }

        [XmlAttribute("d")]
        public string Description { get; set; }

        [XmlIgnore]
        public int Flag { get; set; } = 1;

        [XmlIgnore]
        public bool Clickable { get; set; } = true;

        [XmlIgnore]
        public int Id { get; set; }

        [XmlIgnore]
        public uint VirtualKey { get; set; }

        [XmlAttribute("i")]
        public string IntervalStr
        {
            get => Interval.ToString();
            set => Interval = int.TryParse(value, out int result) ? result : 0;
        }

        [XmlAttribute("f")]
        public string FlagStr
        {
            get => Flag.ToString();
            set => Flag = int.TryParse(value, out int result) ? result : 1;
        }

        [XmlAttribute("c")]
        public string ClickableStr
        {
            get => Clickable ? "1" : "0";
            set => Clickable = value == "1";
        }
    }
}
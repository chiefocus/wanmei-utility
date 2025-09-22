using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;

namespace TimerUtility.Models
{
    [XmlRoot("r")]
    public class Settings
    {
        public static readonly Guid UserDefinedBossId = new Guid("00000000-0000-0000-0000-000000000001");

        [XmlElement("u")]
        public Profile Profile { get; set; } = new Profile();

        [XmlIgnore]
        public Dictionary<Guid, Instance> InstanceDic => Instances.ToDictionary(k => k.Id, v => v);

        [XmlElement("h")]
        public List<Instance> Instances { get; set; } = new List<Instance>();

        [XmlElement("udb")]
        public Boss UserDefinedBoss { get; set; } = new Boss() { Id = UserDefinedBossId };

        [XmlElement("p")]
        public Preference Preference { get; set; } = new Preference();
    }

    public class Preference
    {
        [XmlElement("l")]
        [DefaultValue(null)]
        public Point? Location { get; set; }
        [XmlElement("s")]
        [DefaultValue(null)]
        public Size? ClientSize { get; set; }

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

        [XmlIgnore]
        public bool Preservable { get; set; } = false;

        [XmlAttribute("s")]
        public string PreservableStr
        {
            get => Preservable ? "1" : "0";
            set => Preservable = value == "1";
        }

        [XmlIgnore]
        public bool Shortcutable { get; set; } = true;

        [XmlAttribute("k")]
        public string ShortcutableStr
        {
            get => Shortcutable ? "1" : "0";
            set => Shortcutable = value == "1";
        }
    }

    public class Instance
    {
        [XmlIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlAttribute("n")]
        public string Name { get; set; }

        [XmlIgnore]
        public Dictionary<Guid, Boss> BossDic => Bosses.ToDictionary(k => k.Id, v => v);

        [XmlElement("b")]
        public List<Boss> Bosses { get; set; } = new List<Boss>();

        [XmlIgnore]
        public bool Default { get; set; }

        [XmlAttribute("df")]
        [DefaultValue("0")]
        public string DefaultStr
        {
            get => Default ? "1" : "0";
            set => Default = value == "1";
        }
    }

    public class Boss
    {
        [XmlIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlIgnore]
        public Guid InstanceId { get; set; }

        [XmlIgnore]
        public string InstanceName { get; set; }

        [XmlAttribute("n")]
        public string Name { get; set; }

        [XmlIgnore]
        public Dictionary<Guid, Skill> SkillDic => Skills.ToDictionary(k => k.Id, v => v);

        [XmlElement("s")]
        public List<Skill> Skills { get; set; } = new List<Skill>();

        [XmlIgnore]
        public bool Default { get; set; }

        [XmlAttribute("df")]
        [DefaultValue("0")]
        public string DefaultStr
        {
            get => Default ? "1" : "0";
            set => Default = value == "1";
        }
    }

    public class Skill
    {
        [XmlIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlIgnore]
        public Guid BossId { get; set; }

        [XmlIgnore]
        public Guid InstanceId { get; set; }

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
        public int Flag { get; set; } = 1; //是否开打计时

        [XmlIgnore]
        public bool Clickable { get; set; } = true;

        [XmlIgnore]
        public int Key { get; set; }

        [XmlIgnore]
        public uint VirtualKey { get; set; }

        [XmlAttribute("i")]
        public string IntervalStr
        {
            get => Interval.ToString();
            set => Interval = int.TryParse(value, out int result) ? result : 0;
        }

        [XmlAttribute("f")]
        [DefaultValue("1")]
        public string FlagStr
        {
            get => Flag.ToString();
            set => Flag = int.TryParse(value, out int result) ? result : 1;
        }

        [XmlAttribute("c")]
        [DefaultValue("1")]
        public string ClickableStr
        {
            get => Clickable ? "1" : "0";
            set => Clickable = value == "1";
        }

        [XmlAttribute("a")]
        [DefaultValue("")]
        public string Affiliate { get; set; } = "";
    }
}
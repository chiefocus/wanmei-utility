using System.Collections.Generic;

namespace Timer.Models
{
    public class Boss
    {
        public string InstanceName { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}

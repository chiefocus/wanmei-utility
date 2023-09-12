using System.Collections.Generic;

namespace WindowsFormsApp2.Models
{
    public class Boss
    {
        public string InstanceName { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}

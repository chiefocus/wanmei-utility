using System.Collections.Generic;

namespace TimerUtility.Models
{
    public class Instance
    {
        public string Name { get; set; }
        public List<Boss> Bosses { get; set; } = new List<Boss>();
    }
}

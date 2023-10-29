namespace WindowsFormsApp2.Models
{
    public class Skill
    {
        public string InstanceName { get; set; }
        public string BossName { get; set; }
        public string Name { get; set; } = "计时";
        public string Reset { get; set; } = "清除";
        public string Label1Text { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public int Flag { get; set; } = 1;//0-开打计时,1-非开打计时
    }
}

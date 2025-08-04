namespace TimerUtility.Models
{
    public class Profile
    {
        public bool PlusFlag { get; set; }
        public bool MinusFlag { get; set; }
        public bool MillisecondsFlag { get; set; }
        public int Offset { get; set; } = 1000; // in milliseconds
    }
}

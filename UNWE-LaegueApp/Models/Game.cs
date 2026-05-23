namespace UNWE_LaegueApp.Models
{
    public class Game : CommonEntity
    {
        public int TeamOneId { get; set; }
        public Team? TeamOne { get; set; } 

        public int TeamTwoId { get; set; }
        public Team? TeamTwo { get; set; } 

        public int TeamOneScore { get; set; }
        public int TeamTwoScore { get; set; }

        public DateTime? StartTime { get; set; }
        public int Round { get; set; }
        public bool IsPlayed { get; set; }
    }
}
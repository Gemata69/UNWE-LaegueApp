using System.ComponentModel.DataAnnotations.Schema; 

namespace UNWE_LaegueApp.Models
{
    public class League : CommonEntity
    {
        public string Name { get; set; } = string.Empty;

        public int MaxTeams { get; set; } = 16;

        public int SeasonOneId { get; set; }

        [ForeignKey("SeasonOneId")] 
        public Season SeasonOne { get; set; } 

        public int SeasonTwoId { get; set; }

        [ForeignKey("SeasonTwoId")] 
        public Season SeasonTwo { get; set; } 

        public List<Team> Teams { get; set; } = new();
    }
}
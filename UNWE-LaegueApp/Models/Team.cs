namespace UNWE_LaegueApp.Models
{
    public class Team : CommonEntity
    {
        
        public List<Player> Players { get; set; } = new List<Player>();


        public string Name { get; set; } = string.Empty;

        public Player? Captain { get; set; }

        public int? CaptainId { get; set; }

    }
}

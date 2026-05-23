namespace UNWE_LaegueApp.Models
{
    public class Season : CommonEntity
    {
            

        public List<Game> Games { get; set; } = new List<Game>();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}

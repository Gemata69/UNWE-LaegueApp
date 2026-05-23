namespace UNWE_LaegueApp.Models
{
    public class LogItem
    {
        public int Id { get; set; }

        public string TableName { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public DateTime Time { get; set; }

    }
}

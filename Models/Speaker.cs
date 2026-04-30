namespace ConferenceManager.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string Name { get; set; }
        public string Bio { get; set; }
    }
}

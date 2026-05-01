namespace ConferenceManager.Models
{
    public class Attendee
    {

        public int Id { get; set; } 


        public string Name { get; set; }

        //links to an event
        public int EventId { get; set; }

        //links to user in the system

        public string? UserId { get; set; }
    }
}

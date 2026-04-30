namespace ConferenceManager.Models
{
    public class Attendee
    {

        public int Id { get; set; } 

        //links to an event
        public int EventId { get; set; }

        //links to user in the system

        public int UserId { get; set; }
    }
}

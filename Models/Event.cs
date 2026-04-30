namespace ConferenceManager.Models
{
    public class Event
    {
        public int id { get; set; }

        public string title { get; set; }

        public string date { get; set; }

        public string venue {  get; set; }

        public string description { get; set; }

        public string category {  get; set; }

        public List<Attendee> attendee = new List<Attendee>();

        public List<Speaker> speaker = new List<Speaker>();


    }
}

using ConferenceManager.Models;
using ConferenceManager.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConferenceManager.Services
{
    public interface IEventService
    {
        public IEnumerable<Event> GetEvents();

        public Event? GetEventById(int id);

        public void AddEvent(Event newEvent);

        public List<Attendee> GetAttendees(int eventId);

        public List<Speaker> GetSpeakers(int eventId);

        public bool AddAttendee(int eventId, string userId, Attendee attendee);

        //public void GetAttendeeById(int attendeeId);




    }

    public class EventService : IEventService
    {
        private readonly EventModel _eventModel;

        public EventService(EventModel eventModel)
        {
            _eventModel = eventModel;
        }
        public IEnumerable<Event> GetEvents()
        {
            return _eventModel.GetAllEvents();
        }

        public Event? GetEventById(int id)
        {
            return _eventModel.GetEventById(id);
        }

        public void AddEvent(Event newEvent)
        {
            _eventModel.AddEvent(newEvent);
        }

        public List<Attendee> GetAttendees(int eventId)
        {
            var ev = _eventModel.GetEventById(eventId);
            if (ev == null) throw new Exception("Event not FOUND");
            return ev?.attendees ?? new List<Attendee>();

        }

        public List<Speaker> GetSpeakers(int eventId)
        {

            var ev = _eventModel.GetEventById(eventId);
            if (ev == null) throw new Exception("Event Not FOUND");
            return ev?.speakers ?? new List<Speaker>();
        }

        public bool AddAttendee(int eventId, string userId , Attendee attendee)
        {
            // 1. Get the MASTER list of all events
            var allEvents = _eventModel.GetAllEvents();

            // 2. Find the specific event inside THAT list
            var ev = allEvents.FirstOrDefault(e => e.id == eventId);

            if (ev == null) return false;

            ev.attendees ??= new List<Attendee>();

            if (ev.attendees.Any(a => a.UserId == userId)) return false;

            // 3. Set the data
            attendee.EventId = eventId;
            attendee.UserId = userId;
            attendee.Id = ev.attendees.Count + 1;

            ev.attendees.Add(attendee);

            // 4. CRITICAL: Save the modified master list back to the JSON file
            _eventModel.UpdateEvents(allEvents);

            return true;

        }





    }



}

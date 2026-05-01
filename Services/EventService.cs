using ConferenceManager.Models;
using ConferenceManager.Repository;
using Microsoft.Extensions.Logging;
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

        public Attendee? GetAttendeeById(int attendeeId);




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

        public Attendee? GetAttendeeById(int attendeeId)
        {
            var allEvents = _eventModel.GetAllEvents();
            foreach (var ev in allEvents)
            {
                if (ev.attendees != null)
                {
                    var foundAttendee = ev.attendees.FirstOrDefault(a => a.Id == attendeeId);
                    if (foundAttendee != null)
                    {
                        return foundAttendee; // Found them!
                    }
                }
            }

            return null; // No attendee found with that ID in any event

        }

        public List<Speaker> GetSpeakers(int eventId)
        {

            var ev = _eventModel.GetEventById(eventId);
            if (ev == null) throw new Exception("Event Not FOUND");
            return ev?.speakers ?? new List<Speaker>();
        }

        public bool AddAttendee(int eventId, string userId , Attendee attendee)
        {
            var allEvents = _eventModel.GetAllEvents();

            var ev = allEvents.FirstOrDefault(e => e.id == eventId);

            if (ev == null) return false;

            ev.attendees ??= new List<Attendee>();

            if (ev.attendees.Any(a => a.UserId == userId)) return false;

            int nextId = ev.attendees.Count > 0 ? ev.attendees.Max(a => a.Id) + 1 : 1;

            // Set the data

            attendee.EventId = eventId;
            attendee.UserId = userId;
            attendee.Id = nextId;

            ev.attendees.Add(attendee);

            //  Save the modified master list back to the JSON file
            _eventModel.UpdateEvents(allEvents);

            return true;

        }





    }



}

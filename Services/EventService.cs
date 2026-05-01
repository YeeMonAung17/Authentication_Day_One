using ConferenceManager.Models;
using ConferenceManager.Repository;

namespace ConferenceManager.Services
{
    public interface IEventService
    {
        public IEnumerable<Event> GetEvents();

        public Event? GetEventById(int id);

        public void AddEvent(Event newEvent);

        public List<Attendee> GetAttendees(int eventId);

        public List<Speaker> GetSpeakers(int eventId);



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



    }



}

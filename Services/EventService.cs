using ConferenceManager.Models;
using ConferenceManager.Repository;

namespace ConferenceManager.Services
{
    public interface IEventService
    {
        public IEnumerable<Event> GetEvents();

        public Event? GetEventById(int id);

        public void AddEvent(Event newEvent);



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
            _eventModel.GetAttendees(eventId);

        }


    }



}

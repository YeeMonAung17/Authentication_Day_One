using ConferenceManager.Models;
using ConferenceManager.Repository;

namespace ConferenceManager.Services
{
    public interface IEventService
    {
        public IEnumerable<Event> GetEvents();

        public Event? GetEventById(int id);


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



    }



}

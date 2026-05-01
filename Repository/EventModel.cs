using ConferenceManager.Models;
using System.Text.Json;

namespace ConferenceManager.Repository
{


    public class EventModel
    {

        private string _filePath = ".\\Resources\\EventData.json";

        private readonly List<Event> _events = new List<Event>();

        public List<Event> GetAllEvents()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
        }

        public void UpdateEvents(List<Event> events)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(events, options);
            File.WriteAllText(_filePath, updatedJson);
        }

        public Event? GetEventById(int id)
        {
            var events = GetAllEvents();
            return events.FirstOrDefault(a => a.id == id);
        }

        public void AddEvent(Event newEvent)
        {
            var currentEvents = GetAllEvents();

            currentEvents.Add(newEvent);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(currentEvents, options);

            File.WriteAllText(_filePath, updatedJson);
        }


    }










}

using ConferenceManager.Models;
using System.Text.Json;

namespace ConferenceManager.Repository
{


    public class EventModel
    {

        private string _filePath = ".\\Resources\\EventData.json";

        public List<Event> GetAllEvents()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
        }


        public Event? GetEventById(int id)
        {
            var events = GetAllEvents();
            return events.FirstOrDefault(a => a.id == id);
        }
    }










}

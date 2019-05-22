using System.Collections.Generic;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface IEventService
    {
        bool AddEvent(Event e, int userId);
        bool EditEvent(Event e, int userId);
        bool RemoveEvent(int id, int userId);
        IEnumerable<Event> GetEvents(int calendarId, int userId);
        Event GetEvent(int id, int userId);
    }
}

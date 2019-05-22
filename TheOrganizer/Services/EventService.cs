using System.Collections.Generic;
using System.Linq;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class EventService : IEventService
    {
        private readonly TheOrganizerDBContext _db;

        public EventService(TheOrganizerDBContext db)
        {
            _db = db;
        }

        public bool AddEvent(Event e, int userId)
        {
            if (!CheckEventValidity(e) || !CheckCalendarAccess(e.CalendarId, userId))
                return false;
            _db.Add(e);
            _db.SaveChanges();

            e.Calendar = null;
            return true;
        }

        public bool EditEvent(Event e, int userId)
        {
            var oldEvent = _db.Events.Find(e.Id);
            if (oldEvent == null ||
                !CheckEventValidity(e) ||
                !CheckCalendarAccess(e.CalendarId, userId))
                return false;
            _db.Entry(oldEvent).CurrentValues.SetValues(e);
            _db.SaveChanges();
            return true;
        }

        public Event GetEvent(int id, int userId)
        {
            var e = _db.Events.Find(id);
            if (e != null && CheckCalendarAccess(e.CalendarId, userId))
                return e;
            return null;
        }

        public IEnumerable<Event> GetEvents(int calendarId, int userId)
        {
            if (!CheckCalendarAccess(calendarId, userId))
                return null;
            return _db.Events.Where(e => e.CalendarId == calendarId);
        }

        public bool RemoveEvent(int id, int userId)
        {
            var e = _db.Events.Find(id);
            if (e == null || !CheckCalendarAccess(e.CalendarId, userId))
                return false;
            _db.Events.Remove(e);
            _db.SaveChanges();
            return true;
        }

        private bool CheckEventValidity(Event e)
        {
            return !(e.Start > e.End || string.IsNullOrWhiteSpace(e.Title));
        }

        private bool CheckCalendarAccess(int calendarId, int userId)
        {
            var calendar = _db.Calendars.Find(calendarId);
            if (calendar == null || calendar.OwnerId != userId)
                return false;
            return true;
        }
    }
}

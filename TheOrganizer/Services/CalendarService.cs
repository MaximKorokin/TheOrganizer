using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly TheOrganizerDBContext _db;

        public CalendarService(TheOrganizerDBContext db)
        {
            _db = db;
        }

        public Calendar AddCalendar(Calendar calendar)
        {
            if (calendar != null && !string.IsNullOrWhiteSpace(calendar.Title))
            {
                _db.Calendars.Add(calendar);
                _db.SaveChanges();
                return calendar;
            }
            return null;
        }

        public bool EditCalendar(Calendar calendar)
        {
            if (string.IsNullOrWhiteSpace(calendar.Title))
                return false;

            Calendar oldCalendar = _db.Calendars.Find(calendar.Id);

            if (oldCalendar != null && oldCalendar.OwnerId == calendar.OwnerId)
            {
                _db.Entry(oldCalendar).CurrentValues.SetValues(calendar);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Calendar GetCalendar(int calendarId, int ownerId)
        {
            Calendar calendar = _db.Calendars.Find(calendarId);
            if (calendar.OwnerId == ownerId)
                return calendar;
            return null;
        }

        public IEnumerable<Calendar> GetCalendars(int ownerId)
        {
            return _db.Calendars.Where(x => x.OwnerId == ownerId);
        }

        public bool RemoveCalendar(int calendarId, int ownerId)
        {
            Calendar calendar = _db.Calendars.Find(calendarId);

            if (calendar != null && calendar.OwnerId == ownerId)
            {
                _db.Calendars.Remove(calendar);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

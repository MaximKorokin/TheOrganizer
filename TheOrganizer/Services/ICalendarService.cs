using System.Collections.Generic;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface ICalendarService
    {
        bool AddCalendar(Calendar calendar);

        bool EditCalendar(Calendar calendar);

        bool RemoveCalendar(int calendarId, int ownerId);

        Calendar GetCalendar(int calendarId, int ownerId);

        IEnumerable<Calendar> GetCalendars(int ownerId);
    }
}

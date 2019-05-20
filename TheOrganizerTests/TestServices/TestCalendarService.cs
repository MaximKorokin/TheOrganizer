using System;
using System.Collections.Generic;
using System.Text;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    internal class TestCalendarService : ICalendarService
    {
        public Calendar AddCalendar(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public bool EditCalendar(Calendar calendar)
        {
            throw new NotImplementedException();
        }

        public Calendar GetCalendar(int calendarId, int ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Calendar> GetCalendars(int ownerId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCalendar(int calendarId, int ownerId)
        {
            throw new NotImplementedException();
        }
    }
}

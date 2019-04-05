using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public bool AddEvent(Event e)
        {
            if (!CheckEventValidity(e))
                return false;
            _db.Add(e);
            _db.SaveChanges();
            return true;
        }

        public bool EditEvent(Event e)
        {
            if (_db.Events.Find(e.Id) == null ||
                !CheckEventValidity(e))
                return false;
            _db.Add(e);
            _db.SaveChanges();
            return true;
        }

        public Event GetEvent(int id, int userId)
        {
            var e = _db.Events.Find(id);
            if (e != null && e.OwnerId == userId)
                return e;
            return null;
        }

        public IEnumerable<Event> GetEvents(int userId)
        {
            return _db.Events.Where(e => e.OwnerId == userId);
        }

        public bool RemoveEvent(int id, int userId)
        {
            var e = _db.Events.Find(id);
            if (e == null || e.OwnerId != userId)
                return false;
            _db.Events.Remove(e);
            _db.SaveChanges();
            return true;
        }

        private bool CheckEventValidity(Event e)
        {
            return !(//_db.Users.Find(e.OwnerId) == null ||
                     e.StartTime > e.EndTime ||
                     string.IsNullOrWhiteSpace(e.Title));
        }
    }
}

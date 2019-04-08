using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;
using ToDo = TheOrganizer.Model.Task;

namespace TheOrganizer.Services
{
    public class ToDoService : IToDoService
    {
        private readonly TheOrganizerDBContext _db;

        public ToDoService(TheOrganizerDBContext db)
        {
            _db = db;
        }
        public bool AddToDo(ToDo task)
        {
            if (task != null)
            {
                _db.Tasks.Add(task);
                _db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool EditToDo(ToDo task)
        {
            ToDo OldTask = _db.Tasks.Find(task.Id);

            if (OldTask != null && OldTask.OwnerId == task.OwnerId)
            {
                _db.Entry(OldTask).CurrentValues.SetValues(task);
                _db.SaveChanges();
                return true;
            }
            else return false;
        }

        public ToDo GetToDo(int ToDoId, int OwnerId)
        {
            return _db.Tasks.Where(t => t.Id == ToDoId && t.OwnerId == OwnerId).FirstOrDefault();
        }

        public IEnumerable<ToDo> GetToDos(int OwnerId)
        {
            return _db.Tasks.Where(t => t.OwnerId == OwnerId);
        }

        public ToDo IsToDoDone(int ToDoId, int OwnerId)
        {
            return _db.Tasks.Where(t => t.Id == ToDoId && t.OwnerId == OwnerId).FirstOrDefault();            
        }

        public bool RemoveToDo(int ToDoId, int OwnerId)
        {
            var task = _db.Tasks.Find(ToDoId);
            if (task == null || task.OwnerId != OwnerId)
                return false;
            _db.Tasks.Remove(task);
            _db.SaveChanges();
            return true;
        }
    }
}

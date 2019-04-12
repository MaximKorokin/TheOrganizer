using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class TodoService : ITodoService
    {
        private readonly TheOrganizerDBContext _db;

        public TodoService(TheOrganizerDBContext db)
        {
            _db = db;
        }
        public bool AddTodo(Todo task)
        {
            //if (task != null)
            //{
            //    _db.Todos.Add(task);
            //    _db.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public bool EditTodo(Todo task)
        {
            //Todo OldTask = _db.Todos.Find(task.Id);

            //if (OldTask != null && OldTask.OwnerId == task.OwnerId)
            //{
            //    _db.Entry(OldTask).CurrentValues.SetValues(task);
            //    _db.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public Todo GetTodo(int ToDoId, int OwnerId)
        {
            return null; //_db.Todos.Where(t => t.Id == ToDoId && t.OwnerId == OwnerId).FirstOrDefault();
        }

        public IEnumerable<Todo> GetTodos(int OwnerId)
        {
            return null; //_db.Todos.Where(t => t.OwnerId == OwnerId);
        }

        public bool RemoveTodo(int ToDoId, int OwnerId)
        {
            //var task = _db.Todos.Find(ToDoId);
            //if (task == null || task.OwnerId != OwnerId)
            //    return false;
            //_db.Todos.Remove(task);
            //_db.SaveChanges();
            return true;
        }
    }
}

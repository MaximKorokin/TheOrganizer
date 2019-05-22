using System.Collections.Generic;
using System.Linq;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly TheOrganizerDBContext _db;

        public TodoListService(TheOrganizerDBContext db)
        {
            _db = db;
        }
        public bool AddTodoList(TodoList todoList)
        {
            if (todoList != null && !string.IsNullOrWhiteSpace(todoList.Title))
            {
                _db.TodoLists.Add(todoList);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditTodoList(TodoList todoList)
        {
            if (string.IsNullOrWhiteSpace(todoList.Title))
                return false;

            TodoList oldTodoList = _db.TodoLists.Find(todoList.Id);

            if (oldTodoList != null && oldTodoList.OwnerId == todoList.OwnerId)
            {
                _db.Entry(oldTodoList).CurrentValues.SetValues(todoList);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public TodoList GetTodoList(int todoListId, int ownerId)
        {
            TodoList todoList = _db.TodoLists.Find(todoListId);
            if (todoList.OwnerId == ownerId)
                return todoList;
            return null;
        }

        public IEnumerable<TodoList> GetTodoLists(int ownerId)
        {
            return _db.TodoLists.Where(x => x.OwnerId == ownerId);
        }

        public bool RemoveTodoList(int todoListId, int ownerId)
        {
            TodoList todoList = _db.TodoLists.Find(todoListId);

            if (todoList != null && todoList.OwnerId == ownerId)
            {
                _db.TodoLists.Remove(todoList);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

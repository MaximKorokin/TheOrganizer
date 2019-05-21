using Microsoft.EntityFrameworkCore;
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
        public bool AddTodo(Todo todo, int ownerId)
        {
            if (todo != null && CheckTodoListAccess(todo.TodoListId, ownerId))
            {
                _db.Todos.Add(todo);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditTodo(Todo todo, int ownerId)
        {
            Todo OldTodo = _db.Todos.Find(todo.Id);
            var currentTodoList = _db.TodoLists.Find(todo.TodoListId);

            if (OldTodo != null && CheckTodoListAccess(currentTodoList.Id, ownerId))
            {
                _db.Entry(OldTodo).CurrentValues.SetValues(todo);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Todo GetTodo(int todoId, int ownerId)
        {
            return _db.Todos.Where(t => t.Id == todoId && t.TodoList.OwnerId == ownerId).FirstOrDefault();
        }

        public IEnumerable<Todo> GetTodos(int todoListId, int ownerId)
        {
            if (!CheckTodoListAccess(todoListId, ownerId))
                return null;
            return _db.Todos.Where(t => t.TodoListId == todoListId);
        }

        public bool RemoveTodo(int todoId, int ownerId)
        {
            var currentTodo = _db.Todos.Find(todoId);
            if (currentTodo == null || !CheckTodoListAccess(currentTodo.TodoListId, ownerId))
                return false;
            _db.Todos.Remove(currentTodo);
            _db.SaveChanges();
            return true;
        }

        private bool CheckTodoListAccess(int todoListId, int ownerId)
        {
            var todoList = _db.TodoLists.Find(todoListId);
            if (todoList == null || todoList.OwnerId != ownerId)
                return false;
            return true;
        }
    }
}
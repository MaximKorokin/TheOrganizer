using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface ITodoService
    {
        bool AddTodo(Todo task);

        bool EditTodo(Todo task);

        bool RemoveTodo(int ToDoId, int OwnerId);

        IEnumerable<Todo> GetTodos(int OwnerId);

        Todo GetTodo(int ToDoId, int OwnerId);
    }
}

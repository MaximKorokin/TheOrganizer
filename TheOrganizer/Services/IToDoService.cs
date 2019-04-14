using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface ITodoService
    {
        bool AddTodo(Todo toDo, int ownerId);

        bool EditTodo(Todo toDo, int ownerId);

        bool RemoveTodo(int toDoId, int ownerId);

        IEnumerable<Todo> GetTodos(int ownerId, int toDoListId);

        Todo GetTodo(int toDoId, int ownerId);
    }
}
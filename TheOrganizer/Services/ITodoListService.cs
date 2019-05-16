using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface ITodoListService
    {
        TodoList AddTodoList(TodoList todoList);

        bool EditTodoList(TodoList todoList);

        bool RemoveTodoList(int todoListId, int ownerId);

        TodoList GetTodoList(int todoListId, int ownerId);

        IEnumerable<TodoList> GetTodoLists(int ownerId);
    }
}

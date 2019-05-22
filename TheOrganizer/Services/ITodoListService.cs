using System.Collections.Generic;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public interface ITodoListService
    {
        bool AddTodoList(TodoList todoList);

        bool EditTodoList(TodoList todoList);

        bool RemoveTodoList(int todoListId, int ownerId);

        TodoList GetTodoList(int todoListId, int ownerId);

        IEnumerable<TodoList> GetTodoLists(int ownerId);
    }
}

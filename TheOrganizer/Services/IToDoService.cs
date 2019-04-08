using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo = TheOrganizer.Model.Task;

namespace TheOrganizer.Services
{
    public interface IToDoService
    {
        bool AddToDo(ToDo task);

        bool EditToDo(ToDo task);

        bool RemoveToDo(int ToDoId, int OwnerId);

        IEnumerable<ToDo> GetToDos(int OwnerId);

        ToDo GetToDo(int ToDoId, int OwnerId);

        ToDo IsToDoDone(int ToDoId, int OwnerId);
    }
}

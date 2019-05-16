using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;

namespace TheOrganizerTests.TestServices
{
    internal class TestToDoService : ITodoService
    {
        private List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Name1",
                Email = "Email1",
                Password = "Password1",
            },
            new User
            {
                Id = 2,
                Name = "Name2",
                Email = "Email2",
                Password = "Password2",
            },
            new User
            {
                Id = 3,
                Name = "Name3",
                Email = "Email3",
                Password = "Password3",
            },
        };
        private List<Todo> ToDos { get; set; } = new List<Todo>
        {
            new Todo
            {
                Id = 1,
                Text = "Text1",
                TodoListId = 1,
                TodoList = new TodoList()
                {
                    OwnerId = 1
                }
            },
            new Todo
            {
                Id = 2,
                Text = "Text2",
                TodoListId = 1,
                TodoList = new TodoList()
                {
                    OwnerId = 1
                }
            },
            new Todo
            {
                Id = 3,
                Text = "Text3",
                TodoListId = 2,
                TodoList = new TodoList()
                {
                    OwnerId = 2
                }
            },
        };

        private TodosController _controller;

        public TodosController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                _controller = value;
                Authenticate();
            }
        }



        public bool AddTodo(Todo toDo, int ownerId)
        {
            if (toDo == null)
                return false;

            try
            {
                ToDos.Add(toDo);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool EditTodo(Todo toDo, int ownerId)
        {
            if (toDo == null)
                return false;

            var cur = ToDos.FirstOrDefault(t => t.Id == toDo.Id);

            if (cur == null)
                return false;

            return true;
        }

        public bool RemoveTodo(int toDoId, int ownerId)
        {
            var deleted = ToDos.Remove(ToDos.FirstOrDefault(t => t.Id == toDoId));

            return deleted;
        }

        public IEnumerable<Todo> GetTodos(int ownerId, int toDoListId)
        {
            return ToDos.Where(t => t.TodoList.OwnerId == ownerId && t.TodoListId == toDoListId).ToList();
        }

        public Todo GetTodo(int toDoId, int ownerId)
        {
            return ToDos.FirstOrDefault(t => t.TodoList.OwnerId == ownerId && t.Id == toDoId);
        }




        private readonly User userForAuth = new User
        {
            Id = 1,
            Name = "Name1",
            Email = "Email1",
            Password = "Password1",
        };

        private void Authenticate()
        {
            var user = Users.FirstOrDefault(x => x.Email == userForAuth.Email && x.Password == userForAuth.Password);

            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "1")
                    }, "someAuthTypeName")),
                }
            };
        }
    }
}

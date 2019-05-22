using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;
using Xunit;

namespace TheOrganizerTests.ControllersTests
{
    public class TodoControllerTests
    {
        private readonly ITodoService _toDoService;
        private readonly TodosController _controller;

        public TodoControllerTests()
        {
            _toDoService = new TestToDoService();
            _controller = new TodosController(_toDoService);
            ((TestToDoService)_toDoService).Controller = _controller;
        }

        [Fact]
        public void AddTodo()
        {
            var contact = new Todo()
            {
                Text = "Text123",
                TodoListId = 1,
            };

            var result = _controller.AddTodo(contact) as ObjectResult;

            Assert.True(result.Value != null, "result is null");
            Assert.True(((Calendar)result.Value).Title == "Title123", "Text is not Text123");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void EditTodo()
        {
            var contact = new Todo()
            {
                Id = 1,
                Text = "Text",
                TodoListId = 1,
            };

            var result = _controller.EditTodo(contact) as StatusCodeResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void RemoveTodo()
        {
            var contact = new Todo()
            {
                Id = 1
            };

            var result = _controller.RemoveTodo(contact.Id) as StatusCodeResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void GetTodos()
        {
            var toDoListId = 1;

            var result = _controller.GetTodos(toDoListId) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as List<Todo>).Count == 2, "TaDos quantity is not correct");
        }

        [Fact]
        public void GetTodo()
        {
            var toDoId = 1;

            var result = _controller.GetTodo(toDoId) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as Todo).Text == "Text1", "Seleted toDo's text is not correct");
        }
    }
}

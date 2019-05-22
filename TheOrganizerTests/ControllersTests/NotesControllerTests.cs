using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheOrganizer.Controllers;
using TheOrganizer.Model;
using TheOrganizer.Services;
using TheOrganizerTests.TestServices;
using Xunit;

namespace TheOrganizerTests.ControllersTests
{
    public class NotesControllerTests
    {
        private readonly INoteService _noteService;
        private readonly NotesController _controller;

        public NotesControllerTests()
        {
            _noteService = new TestNoteService();
            _controller = new NotesController(_noteService);
            ((TestNoteService)_noteService).Controller = _controller;
        }

        [Fact]
        public void AddNote()
        {
            var note = new Note()
            {
                Title = "Title123",
                Text = "Text"
            };

            var result = _controller.AddNote(note) as ObjectResult;

            Assert.True((Note)result.Value != null, "result is false");
            Assert.True(((Note)result.Value).Title == "Title123", "result is not Title123");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void EditNote()
        {
            var note = new Note()
            {
                Id = 1,
                Title = "Title123"
            };

            var result = _controller.EditNote(note) as ObjectResult;

            Assert.True((Note)result.Value != null, "result is null");
            Assert.True(((Note)result.Value).Title == "Title123", "result is not Title123");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void RemoveNote()
        {
            var note = new Note()
            {
                Id = 1
            };

            var result = _controller.RemoveNote(note.Id) as StatusCodeResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
        }

        [Fact]
        public void GetNotes()
        {
            var notebookId = 1;

            var result = _controller.GetNotes(notebookId) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as List<Note>).Count == 2, "Calendars quantity is not correct");
        }

        [Fact]
        public void GetNote()
        {
            var noteId = 1;

            var result = _controller.GetNote(noteId) as ObjectResult;

            Assert.True(result != null, "result is null");
            Assert.True(result.StatusCode == 200, "Status code is not OK");
            Assert.True((result.Value as Note).Title == "Title1", "Seleted note's title is not correct");
        }
    }
}

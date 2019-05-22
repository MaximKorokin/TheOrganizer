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
    class TestNoteService : INoteService
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
        private List<Note> Notes { get; set; } = new List<Note>
        {
            new Note
            {
                Id = 1,
                Title = "Title1",
                Text = "Text1",
                NotebookId = 1,
                Notebook = new Notebook
                {
                    OwnerId = 1
                }
            },
            new Note
            {
                Id = 2,
                Title = "Title2",
                Text = "Text2",
                NotebookId = 1,
                Notebook = new Notebook
                {
                    OwnerId = 1
                }
            },
            new Note
            {
                Id = 3,
                Title = "Title3",
                Text = "Text3",
                NotebookId = 2,
                Notebook = new Notebook
                {
                    OwnerId = 2
                }
            },
        };

        private NotesController _controller;

        public NotesController Controller
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



        public bool AddNote(Note note, int ownerId)
        {
            if (note == null)
                return false;

            try
            {
                Notes.Add(note);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool EditNote(Note note, int ownerId)
        {
            if (note == null)
                return false;

            var cur = Notes.FirstOrDefault(n => n.Id == note.Id);

            if (cur == null)
                return false;

            return true;
        }

        public bool RemoveNote(int noteId, int ownerId)
        {
            var deleted = Notes.Remove(Notes.FirstOrDefault(n => n.Id == noteId));

            return deleted;
        }

        public IEnumerable<Note> GetNotes(int notebookId, int ownerId)
        {
            return Notes.Where(n => n.Notebook.OwnerId == ownerId && n.NotebookId == notebookId).ToList();
        }

        public Note GetNote(int noteId, int ownerId)
        {
            return Notes.FirstOrDefault(n => n.Notebook.OwnerId == ownerId && n.Id == noteId);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheOrganizer.Model;

namespace TheOrganizer.Services
{
    public class NoteService : INoteService
    {
        private readonly TheOrganizerDBContext _db;

        public NoteService(TheOrganizerDBContext db)
        {
            _db = db;
        }
        public bool AddNote(Note note, int ownerId)
        {
            if (note != null && CheckNotebookAccess(note.NotebookId, ownerId))
            {
                _db.Notes.Add(note);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditNote(Note note, int ownerId)
        {
            var oldNote = _db.Notes.Find(note.Id);
            if (oldNote == null || !CheckNotebookAccess(note.NotebookId, ownerId))
                return false;
            _db.Entry(oldNote).CurrentValues.SetValues(note);
            _db.SaveChanges();
            return true;
        }

        public Note GetNote(int notesId, int ownerId)
        {
            var note = _db.Notes.Find(notesId);
            if (note != null && CheckNotebookAccess(note.NotebookId, ownerId))
                return note;
            return null;
        }

        public IEnumerable<Note> GetNotes(int notebookId, int ownerId)
        {
            if (!CheckNotebookAccess(notebookId, ownerId))
                return null;
            return _db.Notes.Where(x => x.NotebookId == notebookId);
        }

        public bool RemoveNote(int noteId, int ownerId)
        {
            var note = _db.Notes.Find(noteId);
            if (note == null || !CheckNotebookAccess(note.NotebookId, ownerId))
                return false;
            _db.Notes.Remove(note);
            _db.SaveChanges();
            return true;
        }

        private bool CheckNotebookAccess(int noteBookId, int ownerId)
        {
            var noteBook = _db.Notebooks.Find(noteBookId);
            if (noteBook == null || noteBook.OwnerId != ownerId)
                return false;
            return true;
        }
    }
}
